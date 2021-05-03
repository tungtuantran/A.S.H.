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
    public class LampPresenterTest
    {

        private LampPresenter LampPresenter;
        private LampViewTest view;
        private Lamp lamp1;
        private DistanceCalculatorTest brightnessCalculator;
        private DistanceCalculatorTest colorCalculator;
        private DistanceCalculatorTest temperatureCalculator;


        [SetUp]
        public void SetUp()
        {
            DeviceCollection.DeviceCollectionInstance.RegisteredDevices.Clear();

            GameObject controllerGO = new GameObject();
            controllerGO.AddComponent<LampPresenter>();

            lamp1 = new Lamp("standing_lamp1", 1, "Lamp 1");
            view = new LampViewTest();

            brightnessCalculator = new DistanceCalculatorTest();
            colorCalculator = new DistanceCalculatorTest();
            temperatureCalculator = new DistanceCalculatorTest();


            LampPresenter = controllerGO.GetComponent<LampPresenter>();
            LampPresenter.Device = lamp1;
            LampPresenter.View = view;
            LampPresenter.BrightnessCalculator = brightnessCalculator;
            LampPresenter.ColorCalculator = colorCalculator;
            LampPresenter.TemperatureCalculator = temperatureCalculator;
            LampPresenter.temperatureTexture = (Texture2D)Resources.Load("Images/Color/Kelvin Scale");
        }

        [TearDown]
        public void Teardown()
        {
            Object.Destroy(LampPresenter.gameObject);
            //DeviceCollection.DeviceCollectionInstance.RegisteredDevices.Clear();
        }


        // SetSelectedDeviceOnOff
        [Test]
        public void SetSelectedDeviceOn()
        {

            LampPresenter.SetDeviceOnOff();
            Assert.IsTrue(LampPresenter.Device.IsOn);
        }

        [Test]
        public void SetSelectedDeviceOff()
        {
            LampPresenter.SetDeviceOnOff();
            LampPresenter.SetDeviceOnOff();
            Assert.IsFalse(LampPresenter.Device.IsOn);
        }

        // RemoveSelectedDevice
        [Test]
        public void RemoveSelectedDeviceGood()
        {
            // setup device collection
            DeviceCollection.DeviceCollectionInstance.AddRegisteredDevice(lamp1);

            bool removed = false;

            LampPresenter.RemoveDevice();
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

            LampPresenter.RemoveDevice();

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
            LampPresenter.Device = null;
            Assert.Throws<NoDeviceException>(() => LampPresenter.RemoveDevice());
        }

        // AddDevice
        [Test]
        public void AddDeviceGood()
        {
            view.SetAddNameInputFieldText("Lamp 1");
            LampPresenter.AddDevice();
            Assert.True(DeviceCollection.DeviceCollectionInstance.GetRegisteredDeviceByDeviceId(1) != null);
        }

        
        [Test]
        public void AddDeviceNoInput()
        {
            view.SetAddNameInputFieldText("");
            Assert.Throws<NoInputException>(() => LampPresenter.AddDevice());
        }

        [Test]
        public void AddDeviceWhiteSpaceInput()
        {
            view.SetAddNameInputFieldText("  ");
            Assert.Throws<NoInputException>(() => LampPresenter.AddDevice());
        }
        

        // EditNameOfSelectedDevice
        [Test]
        public void EditNameOfDeviceGood()
        {
            string nameInput = "New Lamp";
            view.SetEditNameInputFieldText(nameInput);
            LampPresenter.EditNameOfDevice();
            Assert.True(lamp1.Name.Equals(nameInput));
        }

        [Test]
        public void EditNameOfDeviceNoInput()
        {
            string nameInput = "";
            view.SetEditNameInputFieldText(nameInput);
            LampPresenter.EditNameOfDevice();
            Assert.True(lamp1.Name.Equals("Lamp 1"));
        }

        [Test]
        public void EditNameOfDeviceWhiteSpaceInput()
        {
            string nameInput = " ";

            view.SetEditNameInputFieldText(nameInput);
            LampPresenter.EditNameOfDevice();
            Assert.True(lamp1.Name.Equals("Lamp 1"));
        }

        
        // Set Light Brightness
        [Test]
        public void SetLightBrightnessDefault()
        {
            float brightness = LampPresenter.TestConvertDistanceToBrightnessValue(0f);
            Assert.AreEqual(1.0f, brightness);
        }

        [Test]
        public void SetLightBrightnessGood()
        {
            float brightness = LampPresenter.TestConvertDistanceToBrightnessValue(0.005f);
            Assert.AreEqual(0.5f, brightness);
        }

        [Test]
        public void SetLightBrightnessMaxReached()
        {
            float brightness = LampPresenter.TestConvertDistanceToBrightnessValue(-0.001f);
            Assert.AreEqual(1f, brightness);
        }

        [Test]
        public void SetLightBrightnessMinReached()
        {
            float brightness = LampPresenter.TestConvertDistanceToBrightnessValue(0.01f);
            Assert.AreEqual(0.15f, brightness);
        }

        // Set Light Color
        [Test]
        public void SetLightColorDefault()
        {
            Color color = LampPresenter.TestConvertDistanceToColorValue(0f,0f);
            Assert.AreEqual(new Color(1,1,1), color);
        }

        [Test]
        public void SetLightColorGood()
        {
            Color color = LampPresenter.TestConvertDistanceToColorValue(0.005f, -0.0025f);
            Assert.AreEqual(new Color(0.5f, 1f, 1f), color);
        }

        [Test]
        public void SetLightColorSaturationSetToZero()
        {
            Color color = LampPresenter.TestConvertDistanceToColorValue(0.0f, -0.005f);
            Assert.AreEqual(new Color(1f, 0f, 0f), color);
        }

        [Test]
        public void SetLightColorSaturationSetToZeroAndSetHue()
        {
            Color color = LampPresenter.TestConvertDistanceToColorValue(0.005f, -0.005f);
            Assert.AreEqual(new Color(0f, 1f, 1f), color);
        }

        [Test]
        public void SetLightColorMinSaturationReached()
        {
            Color color = LampPresenter.TestConvertDistanceToColorValue(0.000f, -0.04f);
            Assert.AreEqual(new Color(1f, 0f, 0f), color);
        }

        [Test]
        public void SetLightColorMaxSaturationReached()
        {
            Color color = LampPresenter.TestConvertDistanceToColorValue(0.000f, 0.04f);
            Assert.AreEqual(new Color(1f, 1f, 1f), color);
        }

        // Set Light Temperature
        [Test]
        public void SetLightTemperatureDefault()
        {
            Color temperatureColor = LampPresenter.TestConvertDistanceToTemperatureColorValue(0.0f);
            Assert.AreEqual(new Color(1, 1, 1), temperatureColor);
        }

        [Test]
        public void SetLightTemperatureGood()
        {
            bool colorIsEqual = false;

            Color temperatureColor = LampPresenter.TestConvertDistanceToTemperatureColorValue(0.005f);

            float expectedR = Mathf.Round(0.973f * 10f);
            float expectedG = Mathf.Round(0.737f * 10f);
            float expectedB = Mathf.Round(0.239f * 10f);

            if (expectedR == Mathf.Round(temperatureColor.r * 10f) && expectedG == Mathf.Round(temperatureColor.g * 10f) && expectedB == Mathf.Round(temperatureColor.b * 10f))
            {
                colorIsEqual = true;
            }
            Assert.IsTrue(colorIsEqual);
        }

        [Test]
        public void SetLightTemperatureMaxReached()
        {
            bool colorIsEqual = false;

            Color temperatureColor = LampPresenter.TestConvertDistanceToTemperatureColorValue(20f);

            float expectedR = Mathf.Round(0.992f * 10f);
            float expectedG = Mathf.Round(0.631f * 10f);
            float expectedB = Mathf.Round(0.227f * 10f);

            if (expectedR == Mathf.Round(temperatureColor.r * 10f) && expectedG == Mathf.Round(temperatureColor.g * 10f) && expectedB == Mathf.Round(temperatureColor.b * 10f))
            {
                colorIsEqual = true;
            }
            Assert.IsTrue(colorIsEqual);
        }

        [Test]
        public void SetLightTemperatureMinReached()
        {
            bool colorIsEqual = false;

            Color temperatureColor = LampPresenter.TestConvertDistanceToTemperatureColorValue(-20f);

            float expectedR = Mathf.Round(0.490f * 10f);
            float expectedG = Mathf.Round(0.706f * 10f);
            float expectedB = Mathf.Round(0.855f * 10f);

            if (expectedR == Mathf.Round(temperatureColor.r * 10f) && expectedG == Mathf.Round(temperatureColor.g * 10f) && expectedB == Mathf.Round(temperatureColor.b * 10f))
            {
                colorIsEqual = true;
            }
            Assert.IsTrue(colorIsEqual);
        }
    }

    public class LampViewTest : IDeviceView
    {
        public InputField editNameInputField { get; set; }
        public InputField addNameInputField { get; set; }

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
            //nothing
        }

        public void OnDeviceRemoved()
        {
            //nothing
        }

        public void OnUpdateIsOn(bool isOn)
        {
            //nothing
        }
        public void OnUpdateName(string name)
        {
            //nothing
        }

        public void OnRegisteredDevice(bool registered)
        {
            //nothing
        }

        public void OnUpdateLightColor(Color lightColor)
        {
            //nothing
        }

        public void OnUpdateLightTemperature(Color lightTemperature)
        {
            //nothing
        }

        public void OnUpdateLightBrightness(float lightBrightness)
        {
            //nothing
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

    public class DistanceCalculatorTest : IDistanceCalculator
    {
        public float upwardDistance { get; set; }
        public float forwardDistance { get; set; }
        public float sidewardDistance { get; set; }
        public bool Active { get; set; }
    }
}
