using UnityEngine;

namespace SqdthUtils.VectorMath
{
    public static class VectorMath
    {
        /// <summary>
        /// Removes a direction component from a Vector3. Does not modify the target vector
        /// </summary>
        /// <param name="target"> The target Vector3. </param>
        /// <param name="direction"> The direction to be removed. </param>
        /// <returns> The new Vector3 with direction negated. </returns>
        public static Vector3 NegatedDirection(this Vector3 target, Vector3 direction)
        {
            return target - Vector3.Dot(target, direction.normalized) * direction.normalized;
        }

        /// <summary>
        /// Isolates a direction component of a Vector3. Does not modify the target vector
        /// </summary>
        /// <param name="target"> The target Vector3. </param>
        /// <param name="direction"> The direction to be isolated. </param>
        /// <returns> The new Vector3 with isolated direction. </returns>
        public static Vector3 IsolatedDirection(this Vector3 target, Vector3 direction)
        {
            return Vector3.Dot(target, direction.normalized) * direction.normalized;
        }
        
        /// <summary>
        /// Removes a direction component from a Vector3. Modifies the target vector
        /// </summary>
        /// <param name="target"> The target Vector3. </param>
        /// <param name="direction"> The direction to be removed. </param>
        public static void NegateDirection(this Vector3 target, Vector3 direction)
        {
            target -= Vector3.Dot(target, direction.normalized) * direction.normalized;
        }

        /// <summary>
        /// Isolates a direction component of a Vector3. Modifies the target vector
        /// </summary>
        /// <param name="target"> The target Vector3. </param>
        /// <param name="direction"> The direction to be isolated. </param>
        public static void IsolateDirection(this Vector3 target, Vector3 direction)
        {
            target = Vector3.Dot(target, direction.normalized) * direction.normalized;
        }
    }
}
