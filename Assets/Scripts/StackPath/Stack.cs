using UnityEngine;
using UnityEngine.Events;

namespace StackRunner.StackSystem
{
    public class Stack : MonoBehaviour
    {
        [SerializeField] private Transform stackVisual;
        public Transform StackVisual => stackVisual;

        [SerializeField] private Rigidbody stackRigidbody;
        [SerializeField] private BoxCollider stackCollider;

        [SerializeField] private Rigidbody[] stackParts;

        [SerializeField] private Transform[] playerMoveTargets;
        public Transform[] PlayerMoveTargets => playerMoveTargets;

        [SerializeField] private float moveSpeed;

        private Vector3 moveDirection;
        private bool isMoving;

        public float StackWidth { get; set; }
        public float LastStackXPosition { get; set; }

        public static UnityAction<Stack> OnPlaceStack;

        private void Start()
        {
            SetMovementDirection();

            isMoving = true;

            StackWidth = stackVisual.localScale.x;
        }

        private void Update()
        {
            if(isMoving)
            {
                MoveStack();
            }
        }

        private void SetMovementDirection()
        {
            moveDirection = transform.localPosition.x <= 0f ? Vector3.right : Vector3.left;
        }

        private void MoveStack()
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

            if((moveDirection.x > 0 && transform.position.x >= LastStackXPosition + StackWidth) || (moveDirection.x < 0 && transform.position.x <= LastStackXPosition - StackWidth))
            {
                GameController.Instance.LoseGame();

                isMoving = false;
                stackRigidbody.isKinematic = false;
            }
        }

        public void PlaceStack()
        {
            if((moveDirection.x > 0 && transform.position.x <= LastStackXPosition - StackWidth) || (moveDirection.x < 0 && transform.position.x >= LastStackXPosition + StackWidth))
            {
                GameController.Instance.LoseGame();
                stackRigidbody.isKinematic = false;
            }
            else
            {
                OnPlaceStack?.Invoke(this);
            }

            isMoving = false;
        }

        public void CutStack(float previousStackPosition, float cutPosition)
        {
            stackVisual.gameObject.SetActive(false);
            stackCollider.enabled = false;

            foreach(var stackPart in stackParts)
            {
                stackPart.gameObject.SetActive(true);
                stackPart.transform.position = new Vector3(cutPosition, stackPart.transform.position.y, stackPart.transform.position.z);
            }

            stackParts[1].transform.localScale = new Vector3(transform.position.x - cutPosition + StackWidth / 2f, stackParts[1].transform.localScale.y, stackParts[1].transform.localScale.z);
            stackParts[0].transform.localScale = new Vector3(StackWidth - stackParts[1].transform.localScale.x, stackParts[1].transform.localScale.y, stackParts[1].transform.localScale.z);

            // Cut left
            if(transform.position.x < previousStackPosition)
            {
                stackParts[0].isKinematic = false;

                StackWidth = stackParts[1].transform.localScale.x;
                UpdatePlayerMoveTargets(stackParts[0].transform.position.x + StackWidth / 2f);
            }
            else // Cut right
            {
                stackParts[1].isKinematic = false;

                StackWidth = stackParts[0].transform.localScale.x;
                UpdatePlayerMoveTargets(stackParts[0].transform.position.x - StackWidth / 2f);
            }

            OnPlaceStack?.Invoke(this);
            isMoving = false;
        }

        private void UpdatePlayerMoveTargets(float playerMoveTargetPosX)
        {
            foreach(var playerMoveTarget in playerMoveTargets)
            {
                playerMoveTarget.position = new Vector3(playerMoveTargetPosX, playerMoveTarget.position.y, playerMoveTarget.position.z);
            }
        }
    }
}
