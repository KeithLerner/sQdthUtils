using UnityEngine;

namespace SqdthUtils.Layout
{
    public class ArcLayoutGroup : MonoBehaviour
    {
        [field: SerializeField] [field: Min(.000001f)] 
        public float Radius { get; set; } = 1;

        [field: SerializeField] [field: Min(0)] 
        public float ArcLength { get; set; } = 1;
        
        [field: SerializeField] 
        public Vector3 Offset { get; set; } = Vector3.zero;

        private void LateUpdate()
        {
            int childCount = transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                // Get child transform t
                Transform t = transform.GetChild(i);
                
                // Set t's hide flags
                HideFlags hf = enabled ? HideFlags.NotEditable : HideFlags.None;
                if (t.hideFlags != hf) t.hideFlags = hf;
                
                // Change position
                t.localPosition = Offset +
                    (Vector3)PointOnArc(Radius, ArcLength, (i + .5f) / childCount);
            }
        }

        public static Vector2 PointOnArc(float radius, float arcLength, float rawInput)
        {
            radius = Mathf.Max(radius, .000001f);
            arcLength = Mathf.Max(arcLength, 0);
            rawInput = Mathf.Clamp01(rawInput);
        
            float arcDegs = (arcLength / radius) * (180f / Mathf.PI);
            float arcHalfDegs = arcDegs / 2f;
            float degsAtRawInput = rawInput * arcDegs - arcHalfDegs;
            float arcRads = Mathf.Deg2Rad * (degsAtRawInput + 90f);

            float x = radius * Mathf.Cos(arcRads);
            float y = radius * Mathf.Sin(arcRads);

            return new Vector2(x, y);
        }
         
        #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (Application.isPlaying) return;
            
            LateUpdate();
        }
        #endif
    }
}
