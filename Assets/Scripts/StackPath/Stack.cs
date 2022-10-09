using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

namespace StackRunner.StackSystem
{
    public class Stack : MonoBehaviour
    {
        [SerializeField] private Transform stackVisual;
        public Transform StackVisual => stackVisual;
        [SerializeField] private StackVisual stackVisualController;
        public StackVisual StackVisualController => stackVisualController;

        [SerializeField] private Rigidbody stackRigidbody;
        [SerializeField] private BoxCollider stackCollider;

        [SerializeField] private Rigidbody[] stackParts;

        [SerializeField] private Transform[] playerMoveTargets;
        public Transform[] PlayerMoveTargets => playerMoveTargets;

        [SerializeField] private float moveSpeed;

        public float StackWidth { get; set; }
        public float LastStackXPosition { get; set; }

        public static UnityAction OnPlaceStack;
        public static UnityAction OnFailStack;

        private void Start()
        {
            StackWidth = stackVisual.localScale.x;

            stackVisual.localScale = Vector3.zero;
            stackVisual.DOScale(new Vector3(StackWidth, 1f, 3f), .25f).SetEase(Ease.OutBack);

            StartMovement();
        }

        public void StartMovement()
        {
            transform.DOMoveX((LastStackXPosition - transform.position.x), moveSpeed).SetSpeedBased(true).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo).SetId("Stack");
        }

        public void PlaceStack()
        {
            DOTween.Kill("Stack");

            if(Mathf.Abs(transform.position.x - LastStackXPosition) >= StackWidth)
            {
                GameController.Instance.LoseGame();
                stackRigidbody.isKinematic = false;

                OnFailStack?.Invoke();
            }
            else
            {
                OnPlaceStack?.Invoke();
            }
        }

        public void CutStack(float previousStackPosition, float cutPosition)
        {
            DOTween.Kill("Stack");

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
                Destroy(stackParts[0].gameObject, 4f);
                StackWidth = stackParts[1].transform.localScale.x;
                UpdatePlayerMoveTargets(stackParts[0].transform.position.x + StackWidth / 2f);
            }
            else // Cut right
            {
                stackParts[1].isKinematic = false;
                Destroy(stackParts[1].gameObject, 4f);

                StackWidth = stackParts[0].transform.localScale.x;
                UpdatePlayerMoveTargets(stackParts[0].transform.position.x - StackWidth / 2f);
            }

            OnPlaceStack?.Invoke();
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
