using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace StackRunner.StackSystem
{
    public class StackPath : MonoBehaviour
    {
        [SerializeField] private StackSpawner stackSpawner;

        [SerializeField] private List<Stack> stacks;
        public List<Stack> Stacks => stacks;

        [SerializeField] private List<Transform> playerPath;
        public List<Transform> PlayerPath => playerPath;

        [SerializeField] private float perfectPlacementThreshold;

        public void ProcessLastStack()
        {
            Stack stackToProcess = stacks[stacks.Count - 1];

            AdjustPlacedStack(stackToProcess);

            if(GameController.Instance.CurrentState == GameState.Gameplay)
            {
                UpdatePlayerPath(stackToProcess);
                stackSpawner.SpawnNewStack();
            }
        }

        private void AdjustPlacedStack(Stack stack)
        {
            float stackPlacementOffset = stack.transform.position.x - playerPath[playerPath.Count - 1].position.x;

            if(Mathf.Abs(stackPlacementOffset) <= perfectPlacementThreshold)
            {
                Debug.Log("Perfect placement");

                stack.transform.DOMoveX(playerPath[playerPath.Count - 1].position.x, .2f);
                stack.transform.DOPunchScale(Vector3.one * .1f, .2f, 1, 1).SetEase(Ease.OutBack);

                stack.PlaceStack();
            }
            else if(Mathf.Abs(stackPlacementOffset) <= stack.StackWidth)
            {
                if(stackPlacementOffset >= 0f)
                {
                    stack.CutStack(playerPath[playerPath.Count - 1].position.x, playerPath[playerPath.Count - 1].position.x + stack.StackWidth / 2f);
                }
                else
                {
                    stack.CutStack(playerPath[playerPath.Count - 1].position.x, playerPath[playerPath.Count - 1].position.x - stack.StackWidth / 2f);
                }
            }
            else
            {
                stack.PlaceStack();
            }
        }

        private void UpdatePlayerPath(Stack stack)
        {
            playerPath[playerPath.Count - 1].position = stack.PlayerMoveTargets[0].position;

            playerPath.AddRange(stack.PlayerMoveTargets);
        }
    }
}