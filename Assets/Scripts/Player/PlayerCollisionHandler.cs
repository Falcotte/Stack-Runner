using UnityEngine;

namespace StackRunner.Player
{
    public class PlayerCollisionHandler : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        public PlayerController PlayerController => playerController;
    }
}
