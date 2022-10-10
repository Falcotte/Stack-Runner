using UnityEngine;
using StackRunner.Player;

namespace StackRunner.Collection
{
    public class Diamond : Collectable
    {
        [SerializeField] protected CollectableAnimationController animationController;

        protected override void Interact(PlayerCollisionHandler playerCollisionHandler)
        {
            base.Interact(playerCollisionHandler);

            playerCollisionHandler.PlayerController.CollectDiamond();

            animationController.StopIdleAnimation();
            animationController.PlayCollectionAnimation(playerCollisionHandler.transform);
        }
    }
}
