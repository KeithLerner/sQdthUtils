using UnityEngine;

namespace SqdthUtils.UIUtility
{
    public class Billboard : MonoBehaviour
    {
        public Transform observer;
        [Range(1,10)] public float turnSpeed = 1f;

        public bool horizontalOnly = false;

        private void Start()
        {
            if (observer == null) observer = Camera.main.transform;
        }
    
        void Update()
        {
            if (observer == null) return;
        
            Vector3 targetPos = observer.position;
            if (horizontalOnly) targetPos.y = transform.position.y;
        
            Quaternion rot = Quaternion.LookRotation(
                (targetPos - transform.position).normalized, 
                Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, turnSpeed);
        }
    }
}