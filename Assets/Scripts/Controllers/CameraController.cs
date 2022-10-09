using UnityEngine;
using Cinemachine;
using StackRunner.Player;

namespace StackRunner
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera playerFollowCamera;

        private void OnEnable()
        {
            PlayerController.OnPlayerFall += StopFollowing;
        }

        private void OnDisable()
        {
            PlayerController.OnPlayerFall -= StopFollowing;
        }

        private void StopFollowing()
        {
            playerFollowCamera.m_Follow = null;
        }
    }
}
