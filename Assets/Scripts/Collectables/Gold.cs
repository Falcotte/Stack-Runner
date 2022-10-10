using UnityEngine;
using StackRunner.Player;

namespace StackRunner.Collection
{
    public class Gold : Collectable
    {
        [SerializeField] protected CollectableAnimationController animationController;

        protected override void Interact(PlayerCollisionHandler playerCollisionHandler)
        {
            base.Interact(playerCollisionHandler);

            playerCollisionHandler.PlayerController.CollectGoldCoin();

            animationController.StopIdleAnimation();
            animationController.PlayCollectionAnimation(playerCollisionHandler.transform);
        }
    }
}
