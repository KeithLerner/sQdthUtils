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
        }

        // Update is called once per frame
        void Update()
        {
            left.activateAction.action.performed +=
                context =>
                {
                    onLeftActivateAction?.Invoke(context);
                };
        
            right.activateAction.action.performed +=
                context =>
                {
                    onRightActivateAction?.Invoke(context);
                };
        }

        public void DebugCallbackContext(InputAction.CallbackContext context)
        {
            Debug.Log(context.action.name);
        }

    }
}
