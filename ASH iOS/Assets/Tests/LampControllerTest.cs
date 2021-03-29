using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.XR.ARFoundation;
using UnityEditor;

namespace Tests
{
    public class LampControllerTest
    {
        
        private LampController lampController;
        private Device lamp1;

        [SetUp]
        public void SetUp()
        {
            DeviceCollection.DeviceCollectionInstance.RegisteredDevices.Clear();

            GameObject controllerGO = new GameObject();
            controllerGO.AddComponent<LampController>();

            lamp1 = new Lamp("standing_lamp1", 1, "Lamp 1");

            lampController = controllerGO.GetComponent<LampController>();
            //lampController.view = controller.GetComponent<LampViewTest>();
            lampController.Device = lamp1;
            lampController.View = new LampViewTest();
        }

        [TearDown]
        public void Teardown()
        {
            Object.Destroy(lampController.gameObject);
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

        /*

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
        

        // SelectDeviceByCurrentlyTrackedDevice
        [Test]
        public void SelectDeviceByCurrentlyTrackedDeviceGood()
        {
            //setup device collection
            DeviceCollection.DeviceCollectionInstance.AddRegisteredDevice(lamp1);

            ImageTracking.deviceId = 1;
            //lampController.SelectDeviceByCurrentlyTrackedDevice();

            Assert.True(lampController.Device.Equals(lamp1));
        }
        
        [Test]
        public void SelectDeviceByCurrentlyTrackedDeviceNoDeviceTracked()
        {
            ImageTracking.deviceId = 0;
            Assert.Throws<NoDeviceException>(() => lampController.SelectDeviceByCurrentlyTrackedDevice());
        }
        */

        /*
        // AddCurrentlyTrackedDevice
        [Test]
        public void AddCurrentlyTrackedDeviceGood()
        {
            ImageTracking.deviceId = lamp1.id;
            ImageTracking.deviceName = lamp1.deviceName;

            string nameInput = lamp1._name;

            lampController.AddCurrentlyTrackedDevice(nameInput);

            Assert.True(DeviceCollection.DeviceCollectionInstance.GetRegisteredDeviceByDeviceId(1) != null);
        }

        [Test]
        public void AddCurrentlyTrackedDeviceNoInput()
        {
            ImageTracking.deviceId = lamp1.Id;
            ImageTracking.deviceName = lamp1.DeviceName;

            string nameInput = "";

            Assert.Throws<NoInputException>(() => lampController.AddDevice());
        }

        [Test]
        public void AddCurrentlyTrackedDeviceWhiteSpaceInput()
        {
            ImageTracking.deviceId = lamp1.Id;
            ImageTracking.deviceName = lamp1.DeviceName;

            string nameInput = " ";

            Assert.Throws<NoInputException>(() => lampController.AddDevice());
        }
        

        // EditNameOfSelectedDevice
        [Test]
        public void EditNameOfSelectedDeviceGood()
        {
            string nameInput = "New Lamp";

            lampController.EditNameOfSelectedDevice(nameInput);

            Assert.True(lamp1._name.Equals(nameInput));
        }

        [Test]
        public void EditNameOfSelectedDeviceNoInput()
        {
            string nameInput = "";

            lampController.EditNameOfSelectedDevice(nameInput);

            Assert.True(lamp1._name.Equals("Lamp 1"));
        }

        [Test]
        public void EditNameOfSelectedDeviceWhiteSpaceInput()
        {
            string nameInput = " ";

            lampController.EditNameOfSelectedDevice(nameInput);

            Assert.True(lamp1._name.Equals("Lamp 1"));
        }
        */
    }

    public class LampViewTest : IDeviceView
    {
        public void OnDeviceAdded(string deviceName)
        {
            throw new System.NotImplementedException();
        }

        public void OnDeviceRemoved()
        {
            throw new System.NotImplementedException();
        }

        public void OnEditDeviceName()
        {
            throw new System.NotImplementedException();
        }
    }
}
