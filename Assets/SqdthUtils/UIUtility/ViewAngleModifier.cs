using UnityEngine;
using UnityEngine.Events;

namespace SqdthUtils.UIUtility
{
    public class ViewAngleModifier : MonoBehaviour
    {
        public Transform observer;
        public float maxAngle;
    
        public UnityEvent OnAngleValidated;
        public UnityEvent OnAngleValid;
        public UnityEvent OnAngleInvalidated;
        public UnityEvent OnAngleInvalid;

        private bool isValid;
    
        // Start is called before the first frame update
        void Start()
        {
            if (observer == null)
            {
                if (Camera.main != null)
                {
                    observer = Camera.main.transform;
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Vector3.Angle(observer.transform.forward, -transform.forward) <
                maxAngle)
            {
                if (!isValid)
                {
                    isValid = true;
                    OnAngleValidated?.Invoke();
                }
                else
                {
                    OnAngleValid?.Invoke();
                }
            }
            else
            {
                if (isValid)
                {
                    isValid = false;
                    OnAngleInvalidated?.Invoke();
                }
                else
                {
                    OnAngleInvalid?.Invoke();
                }
            }
        }
    }
}