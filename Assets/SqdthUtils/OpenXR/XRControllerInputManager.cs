using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using XR = UnityEngine.XR;
using InputDevice = UnityEngine.XR.InputDevice;

namespace SqdthUtils.OpenXR
{
    public class XRControllerInputManager : MonoBehaviour
    {
        public static XRControllerInputManager Singleton;

        public InputDevice LeftInputDevice { get; private set; }
        public InputDevice RightInputDevice { get; private set; }
        
        public ActionBasedController LeftController { get; private set; }
        public ActionBasedController RightController { get; private set; }

        public UnityEvent<InputAction.CallbackContext> onLeftActivateAction;
        public UnityEvent<InputAction.CallbackContext> onRightActivateAction;

        private void Awake()
        {
            // Set up singleton instance
            if (Singleton == null)
                Singleton = this;
            else
            {
                Debug.LogError(
                    "Multiple instances of XRControllerInputManager found, " +
                    "destroying duplicate instances.");
                Destroy(gameObject);
                return;
            }
            
            // TESTING
            var inputDevices = new List<InputDevice>();
            XR.InputDevices.GetDevices(inputDevices);
            foreach (var device in inputDevices)
            {
                Debug.Log($"Device found with name '{device.name}'.");
            }
        }

        private void Update()
        {
            // TESTING
            string debug = "";
            float grip;
            if (LeftInputDevice.TryGetFeatureValue(XR.CommonUsages.grip,
                    out grip))
            {
                debug += $"Left: {grip}\n";
            }
            if (RightInputDevice.TryGetFeatureValue(XR.CommonUsages.grip,
                    out grip))
            {
                debug += $"Right: {grip}";
            }
            Debug.Log(debug);
        }

        public void DebugCallbackContext(InputAction.CallbackContext context)
        {
            Debug.Log(context.action.name);
        }

        private void OnLeftActivateAction(InputAction.CallbackContext context)
        {
            onLeftActivateAction?.Invoke(context);
        }
        
        private void OnRightActivateAction(InputAction.CallbackContext context)
        {
            onRightActivateAction?.Invoke(context);
        }

        private void OnEnable()
        {
            // Get XR input devices
            LeftInputDevice = XR.InputDevices.GetDeviceAtXRNode(XR.XRNode.LeftHand);
            RightInputDevice = XR.InputDevices.GetDeviceAtXRNode(XR.XRNode.RightHand);

            // Get action based controllers
            ActionBasedController[] controllers =
                GetComponentsInChildren<ActionBasedController>();
            foreach (ActionBasedController abc in controllers)
            {
                if (abc.gameObject.name.ToLower().Contains("left"))
                {
                    LeftController = abc;
                }
                else if (abc.gameObject.name.ToLower().Contains("right"))
                {
                    RightController = abc;
                }
            }
            
            // Set up action based controller events if controllers were found
            if (LeftController == null)
            {
                Debug.LogError("Failed to find Left Action Based Controller.");
            }
            else
            {
                LeftController.activateAction.action.performed +=
                    OnLeftActivateAction;
            }
            if (RightController == null)
            {
                Debug.LogError("Failed to find Right Action Based Controller.");
            }
            else
            {
                RightController.activateAction.action.performed +=
                    OnRightActivateAction;
            }
        }

        private void OnDisable()
        {
            LeftController.activateAction.action.performed -= 
                OnLeftActivateAction;
            RightController.activateAction.action.performed -= 
                OnRightActivateAction;
        }
    }
}
