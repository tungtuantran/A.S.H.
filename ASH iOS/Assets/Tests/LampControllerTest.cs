using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.XR.ARFoundation;

namespace Tests
{
    public class LampControllerTest
    {
        /*
        private LampController lampController;
        private Device lamp1;
        
        [SetUp]
        public void SetUp()
        {
            DeviceCollection.DeviceCollectionInstance.registeredDevices.Clear();

            GameObject root = new GameObject();
            root.AddComponent<LampController>();

            lamp1 = new Lamp("standing_lamp1", 1, "Lamp 1");

            lampController = root.GetComponent<LampController>();
            lampController.SelectedDevice = lamp1;
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
            lampController.SetSelectedDeviceOnOff();

            Assert.IsTrue(lampController.SelectedDevice.isOn);
        }
        
        [Test]
        public void SetSelectedDeviceOff()
        {
            lampController.SetSelectedDeviceOnOff();
            lampController.SetSelectedDeviceOnOff();

            Assert.IsFalse(lampController.SelectedDevice.isOn);
        }

        // RemoveSelectedDevice
        [Test]
        public void RemoveSelectedDeviceGood()
        {
            // setup device collection
            DeviceCollection.DeviceCollectionInstance.AddRegisteredDevice(lamp1);

            bool removed = false;

            lampController.RemoveSelectedDevice();
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

            lampController.RemoveSelectedDevice();

            if (DeviceCollection.DeviceCollectionInstance.registeredDevices.Count == 0)
            {
                removed = true;
            }

            Assert.IsTrue(removed);
        }
        
        [Test]
        public void RemoveSelectedDeviceSelectedDeviceIsNull()
        {
            //set selected device null
            lampController.SelectedDevice = null;

            Assert.Throws<NoDeviceException>(() => lampController.RemoveSelectedDevice());
        }

        // SelectDeviceByCurrentlyTrackedDevice
        [Test]
        public void SelectDeviceByCurrentlyTrackedDeviceGood()
        {
            //setup device collection
            DeviceCollection.DeviceCollectionInstance.AddRegisteredDevice(lamp1);

            ImageTracking.deviceId = 1;
            lampController.SelectDeviceByCurrentlyTrackedDevice();

            Assert.True(lampController.SelectedDevice.Equals(lamp1));
        }
        
        [Test]
        public void SelectDeviceByCurrentlyTrackedDeviceNoDeviceTracked()
        {
            ImageTracking.deviceId = 0;
            Assert.Throws<NoDeviceException>(() => lampController.SelectDeviceByCurrentlyTrackedDevice());
        }


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
            ImageTracking.deviceId = lamp1.id;
            ImageTracking.deviceName = lamp1.deviceName;

            string nameInput = "";

            Assert.Throws<NoInputException>(() => lampController.AddCurrentlyTrackedDevice(nameInput));
        }

        [Test]
        public void AddCurrentlyTrackedDeviceWhiteSpaceInput()
        {
            ImageTracking.deviceId = lamp1.id;
            ImageTracking.deviceName = lamp1.deviceName;

            string nameInput = " ";

            Assert.Throws<NoInputException>(() => lampController.AddCurrentlyTrackedDevice(nameInput));
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
}
