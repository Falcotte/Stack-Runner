using UnityEngine;
using StackRunner.Player;

namespace StackRunner.Collection
{
    public class Star : Collectable
    {
        [SerializeField] protected CollectableAnimationController animationController;

        protected override void Interact(PlayerCollisionHandler playerCollisionHandler)
        {
            base.Interact(playerCollisionHandler);

            playerCollisionHandler.PlayerController.CollectStar();

            animationController.StopIdleAnimation();
            animationController.PlayCollectionAnimation(playerCollisionHandler.transform);
        }
    }
}
