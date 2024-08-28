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

        public float gripL;
        public float gripR;

        public InputDevice LeftInputDevice { get; private set; }
        public InputDevice RightInputDevice { get; private set; }

        public UnityEvent<InputAction.CallbackContext> onLeftActivateAction;
        public UnityEvent<InputAction.CallbackContext> onRightActivateAction;

        private void Start()
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
            
            // Get XR input devices
            LeftInputDevice = XR.InputDevices.GetDeviceAtXRNode(XR.XRNode.LeftHand);
            RightInputDevice = XR.InputDevices.GetDeviceAtXRNode(XR.XRNode.RightHand);
            
            Debug.Log(LeftInputDevice.name);
            Debug.Log(RightInputDevice.name);
            
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
            LeftInputDevice.TryGetFeatureValue(XR.CommonUsages.grip,
                out gripL);
            RightInputDevice.TryGetFeatureValue(XR.CommonUsages.grip,
                out gripR);
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
    }
}
