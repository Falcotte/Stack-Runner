using UnityEngine;

namespace StackRunner.StackSystem
{
    public class FinishStack : MonoBehaviour
    {
        [SerializeField] private Transform finishPlayerMoveTarget;
        public Transform FinishPlayerMoveTarget => finishPlayerMoveTarget;
    }
}
