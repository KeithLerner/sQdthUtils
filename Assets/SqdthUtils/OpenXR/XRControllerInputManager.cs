using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace SqdthUtils.OpenXR
{
    public class XRControllerInputManager : MonoBehaviour
    {
        public static XRControllerInputManager Singleton;
        
        public ActionBasedController left;
        public ActionBasedController right;

        public UnityEvent<InputAction.CallbackContext> onLeftActivateAction;
        public UnityEvent<InputAction.CallbackContext> onRightActivateAction;

        private void Awake()
        {
            if (Singleton == null)
                Singleton = this;
            else
            {
                Debug.LogError(
                    "Multiple instances of XRControllerInputManager found, " +
                    "destroying duplicate instances.");
                Destroy(gameObject);
            }
            
            Debug.Log(left.);
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
            left.activateAction.action.performed += OnLeftActivateAction;
            right.activateAction.action.performed += OnRightActivateAction;
        }

        private void OnDisable()
        {
            left.activateAction.action.performed -= OnLeftActivateAction;
            right.activateAction.action.performed -= OnRightActivateAction;
        }
    }
}
