using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.XR.ARFoundation;
using UnityEditor;
using UnityEngine.UI;

namespace Tests
{
    public class LampControllerTest
    {

        private LampController lampController;
        private LampViewTest view;
        private Device lamp1;

        [SetUp]
        public void SetUp()
        {
            DeviceCollection.DeviceCollectionInstance.RegisteredDevices.Clear();

            GameObject controllerGO = new GameObject();
            controllerGO.AddComponent<LampController>();

            lamp1 = new Lamp("standing_lamp1", 1, "Lamp 1");
            view = new LampViewTest();

            lampController = controllerGO.GetComponent<LampController>();
            lampController.Device = lamp1;
            lampController.View = view;
        }

        [TearDown]
        public void Teardown()
        {
            Object.Destroy(lampController.gameObject);
            //DeviceCollection.DeviceCollectionInstance.RegisteredDevices.Clear();
        }


        // SetSelectedDeviceOnOff
        [Test]
        public void SetSelectedDeviceOn()
        {

            lampController.SetDeviceOnOff();
            Assert.IsTrue(lampController.Device.IsOn);
        }

        [Test]
        public void SetSelectedDeviceOff()
        {
            lampController.SetDeviceOnOff();
            lampController.SetDeviceOnOff();
            Assert.IsFalse(lampController.Device.IsOn);
        }

        // RemoveSelectedDevice
        [Test]
        public void RemoveSelectedDeviceGood()
        {
            // setup device collection
            DeviceCollection.DeviceCollectionInstance.AddRegisteredDevice(lamp1);

            bool removed = false;

            lampController.RemoveDevice();
            if (DeviceCollection.DeviceCollectionInstance.GetRegisteredDeviceByDeviceId(1) == null)
            {
                removed = true;
            }

            Assert.IsTrue(removed);
        }

        [Test]
        public void RemoveSelectedDeviceEmptyDeviceCollection()
        {
            bool removed = false;

            lampController.RemoveDevice();

            if (DeviceCollection.DeviceCollectionInstance.RegisteredDevices.Count == 0)
            {
                removed = true;
            }

            Assert.IsTrue(removed);
        }

        [Test]
        public void RemoveSelectedDeviceSelectedDeviceIsNull()
        {
            //set selected device null
            lampController.Device = null;
            Assert.Throws<NoDeviceException>(() => lampController.RemoveDevice());
        }

        // AddDevice
        [Test]
        public void AddDeviceGood()
        {
            view.SetAddNameInputFieldText("Lamp 1");
            lampController.AddDevice();
            Assert.True(DeviceCollection.DeviceCollectionInstance.GetRegisteredDeviceByDeviceId(1) != null);
        }

        
        [Test]
        public void AddDeviceNoInput()
        {
            view.SetAddNameInputFieldText("");
            Assert.Throws<NoInputException>(() => lampController.AddDevice());
        }

        [Test]
        public void AddDeviceWhiteSpaceInput()
        {
            view.SetAddNameInputFieldText("  ");
            Assert.Throws<NoInputException>(() => lampController.AddDevice());
        }
        

        // EditNameOfSelectedDevice
        [Test]
        public void EditNameOfDeviceGood()
        {
            string nameInput = "New Lamp";
            view.SetEditNameInputFieldText(nameInput);
            lampController.EditNameOfDevice();
            Assert.True(lamp1.Name.Equals(nameInput));
        }

        [Test]
        public void EditNameOfDeviceNoInput()
        {
            string nameInput = "";
            view.SetEditNameInputFieldText(nameInput);
            lampController.EditNameOfDevice();
            Assert.True(lamp1.Name.Equals("Lamp 1"));
        }

        [Test]
        public void EditNameOfDeviceWhiteSpaceInput()
        {
            string nameInput = " ";

            view.SetEditNameInputFieldText(nameInput);
            lampController.EditNameOfDevice();
            Assert.True(lamp1.Name.Equals("Lamp 1"));
        }
    
    }

    public class LampViewTest : IDeviceView
    {
        public InputField editNameInputField { get; set; }
        public InputField addNameInputField { get; set; }
        public Device trackedDevice { get; set; }

        public LampViewTest()
        {
            GameObject editNameInputFieldGO = new GameObject();
            editNameInputFieldGO.AddComponent<InputField>();

            GameObject addNameInputFieldGO = new GameObject();
            addNameInputFieldGO.AddComponent<InputField>();

            editNameInputField = editNameInputFieldGO.GetComponent<InputField>();
            addNameInputField = addNameInputFieldGO.GetComponent<InputField>();
        }

        public void OnDeviceAdded(string deviceName)
        {
            Debug.Log(deviceName);
        }

        public void OnDeviceRemoved()
        {
        }

        public void OnEditDeviceName()
        {
        }

        public void SetAddNameInputFieldText(string text)
        {
            addNameInputField.text = text;
        }

        public void SetEditNameInputFieldText(string text)
        {
            editNameInputField.text = text;
        }
    }
}
