using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestSuite
    {
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
            lampController.selectedDevice = lamp1;   
        }

        [TearDown]
        public void Teardown()
        {
            Object.Destroy(lampController.gameObject);
        }

        
        [Test]
        public void SetSelectedDeviceOn()
        {
            lampController.SetSelectedDeviceOnOff();

            Assert.IsTrue(lampController.selectedDevice.isOn);
        }
        
        [Test]
        public void SetSelectedDeviceOff()
        {
            lampController.SetSelectedDeviceOnOff();
            lampController.SetSelectedDeviceOnOff();

            Assert.IsFalse(lampController.selectedDevice.isOn);
        }   

        [Test]
        public void RemoveSelectedDeviceGood()
        {
            //setup device collection
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
            lampController.selectedDevice = null;

            Assert.Throws<NoDeviceException>(() => lampController.RemoveSelectedDevice());
        }
        

        /*
        [Test]
        public void ClearDeviceCollection()
        {
            DeviceCollection.DeviceCollectionInstance.registeredDevices.Clear();
        }
        */

        /*
        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator TestSuiteWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
        */
    }
}
