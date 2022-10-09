using UnityEngine;
using UnityEngine.Events;
using StackRunner.InputSystem;

namespace StackRunner.StackSystem
{
    public class Stack : MonoBehaviour
    {
        [SerializeField] private MeshRenderer stackMeshRenderer;

        [SerializeField] private Transform[] playerMoveTargets;
        public Transform[] PlayerMoveTargets => playerMoveTargets;

        [SerializeField] private float moveSpeed;

        private Vector3 moveDirection;
        private bool isMoving;

        public static UnityAction<Stack> OnPlaceStack;

        private void OnEnable()
        {
            InputController.OnTouchDown += PlaceStack;
        }

        private void OnDisable()
        {
            InputController.OnTouchDown -= PlaceStack;
        }

        private void Start()
        {
            SetMovementDirection();

            isMoving = true;
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
        }

        private void PlaceStack()
        {
            InputController.OnTouchDown -= PlaceStack;
            OnPlaceStack?.Invoke(this);

            isMoving = false;
        }
    }
}
