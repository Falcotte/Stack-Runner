using UnityEngine;
using Cinemachine;

namespace StackRunner
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera playerFollowCamera;

        private void OnEnable()
        {
            GameController.OnGameLose += StopFollowing;
        }

        private void OnDisable()
        {
            GameController.OnGameLose -= StopFollowing;
        }

        private void StopFollowing()
        {
            playerFollowCamera.m_Follow = null;
        }
    }
}
