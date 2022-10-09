using UnityEngine;

namespace StackRunner.Extensions
{
    public static class TransformExtensions
    {
        /// <summary>
        /// Performs a LookAt with a set rotation speed
        /// </summary>
        public static void LookAtGradually(this Transform transform, Transform target, float maxRadiansDelta, bool stableUpVector = false)
        {
            Vector3 targetDirection = target.position - transform.position;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, maxRadiansDelta * Time.deltaTime, 0.0f);

            transform.rotation = Quaternion.LookRotation(newDirection);
            if(stableUpVector)
            {
                transform.localRotation = Quaternion.Euler(0f, transform.localRotation.eulerAngles.y, 0f);
            }
        }

        /// <summary>
        /// Performs a LookAt with a set rotation speed
        /// </summary>
        public static void LookAtGradually(this Transform transform, Vector3 target, float maxRadiansDelta, bool stableUpVector = false)
        {
            Vector3 targetDirection = target - transform.position;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, maxRadiansDelta * Time.deltaTime, 0.0f);

            transform.rotation = Quaternion.LookRotation(newDirection);
            if(stableUpVector)
            {
                transform.localRotation = Quaternion.Euler(0f, transform.localRotation.eulerAngles.y, 0f);
            }
        }
    }
}