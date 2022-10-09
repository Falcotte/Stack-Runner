using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using StackRunner.StackSystem;
using StackRunner.Extensions;

namespace StackRunner.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private StackPath stackPath;

        [SerializeField] private Transform visual;
        [SerializeField] private Rigidbody playerRigidbody;
        [SerializeField] private Animator playerAnimator;

        [SerializeField] private Transform followCameraTarget;

        [SerializeField] private float moveSpeed;
        [SerializeField] private float rotationSpeed;

        private int currentPathNodeIndex;

        private bool isMoving;

        public static UnityAction OnPlayerFall;
        public static UnityAction OnPlayerReachFinishStack;

        private void OnEnable()
        {
            Stack.OnPlaceStack += StartMovement;
        }

        private void OnDisable()
        {
            Stack.OnPlaceStack -= StartMovement;
        }

        private void Update()
        {
            if(isMoving)
            {
                Transform currentTarget = stackPath.PlayerPath[currentPathNodeIndex];

                if(currentTarget != null)
                {
                    Move(currentTarget);
                    visual.LookAtGradually(currentTarget, rotationSpeed);
                }
            }
        }

        private void StartMovement()
        {
            isMoving = true;
            playerAnimator.SetBool("IsMoving", isMoving);
        }

        private void StopMovement()
        {
            isMoving = false;
            playerAnimator.SetBool("IsMoving", isMoving);
        }

        private void Move(Transform target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

            if(Vector3.SqrMagnitude(transform.position - target.position) <= .02f)
            {
                UpdateMoveTarget();
            }
        }

        private void UpdateMoveTarget()
        {
            if(currentPathNodeIndex < stackPath.PlayerPath.Count - 1)
            {
                currentPathNodeIndex++;
            }
            else
            {
                StopMovement();

                if(GameController.Instance.CurrentState == GameState.GameWin)
                {
                    visual.DOLookAt(transform.position + Vector3.back, .5f).OnComplete(() =>
                    {
                        followCameraTarget.DORotate(Vector3.up * 180f, 10f).SetLoops(-1, LoopType.Incremental);
                        playerAnimator.SetTrigger("Dance");
                        OnPlayerReachFinishStack?.Invoke();
                    });
                }
            }
        }
    }
}
