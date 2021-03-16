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
        Device lamp;

        [SetUp]
        public void SetUp()
        {
            /*
            GameObject root = new GameObject();
            root.AddComponent<LampController>();
            lampController = root.GetComponent<LampController>();

            lamp = new Lamp("standing_lamp1", 1, "Lamp 1");
            lampController.selectedDevice = lamp;
            */
        }



        [TearDown]
        public void Teardown()
        {
            //Object.Destroy(lampController.gameObject);
            
        }

        /*
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
        */

        [Test]
        public void RemoveSelectedDevice()
        {

            //setup device collection
            /*
            DeviceCollection.DeviceCollectionInstance.AddRegisteredDevice(lamp);

            bool removed = false;

            lampController.RemoveSelectedDevice();
            if (DeviceCollection.DeviceCollectionInstance.GetRegisteredDeviceByDeviceId(1) == null)
            {
                removed = true;
            }
            
            Debug.Log("anzahl: " + DeviceCollection.DeviceCollectionInstance.registeredDevices.Count);
            Assert.IsTrue(removed);
            */
            //DeviceCollection.DeviceCollectionInstance.RemoveRegisteredDevice(DeviceCollection.DeviceCollectionInstance.GetRegisteredDeviceByDeviceId(1));
            Debug.Log("anzahl: " + DeviceCollection.DeviceCollectionInstance.registeredDevices.Count);
        }



        /*
        // A Test behaves as an ordinary method
        [Test]
        public void TestSuiteSimplePasses()
        {
            // Use the Assert class to test conditions
        }

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
