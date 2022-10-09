using UnityEngine;
using Cinemachine;
using StackRunner.Player;

namespace StackRunner
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera playerFollowCamera;
        [SerializeField] private CinemachineVirtualCamera winCamera;

        private void OnEnable()
        {
            PlayerController.OnPlayerFall += StopFollowing;
            PlayerController.OnPlayerReachFinishStack += EnableWinCamera;
        }

        private void OnDisable()
        {
            PlayerController.OnPlayerFall -= StopFollowing;
            PlayerController.OnPlayerReachFinishStack -= EnableWinCamera;
        }

        private void StopFollowing()
        {
            playerFollowCamera.m_Follow = null;
        }

        private void EnableWinCamera()
        {
            playerFollowCamera.Priority = 0;
            winCamera.Priority = 10;
        }
    }
}
