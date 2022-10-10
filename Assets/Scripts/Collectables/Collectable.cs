using UnityEngine;
using StackRunner.Player;

namespace StackRunner.Collection
{
    public abstract class Collectable : MonoBehaviour
    {
        [SerializeField] protected Collider collectableCollider;

        protected virtual void OnTriggerEnter(Collider other)
        {
            other.gameObject.TryGetComponent(out PlayerCollisionHandler playerCollisionHandler);

            if(playerCollisionHandler)
            {
                Interact(playerCollisionHandler);
            }
        }

        protected virtual void Interact(PlayerCollisionHandler playerCollisionHandler)
        {
            collectableCollider.enabled = false;
        }
    }
}