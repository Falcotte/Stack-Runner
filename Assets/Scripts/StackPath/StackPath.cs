using System.Collections.Generic;
using UnityEngine;
using StackRunner.InputSystem;

namespace StackRunner.StackSystem
{
    public class StackPath : MonoBehaviour
    {
        [SerializeField] private Stack stackPrefab;

        [SerializeField] private float stackSpawnOffset;
        private Vector3 spawnPosition;

        [SerializeField] private List<Transform> playerPath;
        public List<Transform> PlayerPath => playerPath;

        private void OnEnable()
        {
            InputController.OnTouchDown += SpawnNewStack;
            Stack.OnPlaceStack += UpdatePlayerPath;
        }

        private void OnDisable()
        {
            InputController.OnTouchDown -= SpawnNewStack;
            Stack.OnPlaceStack -= UpdatePlayerPath;
        }

        private void Start()
        {
            spawnPosition = Vector3.forward * 12f;
        }

        private void SpawnNewStack()
        {
            Vector3 newSpawnPosition = spawnPosition + Mathf.Sign(Random.Range(-1f, 1f)) * Vector3.right * stackSpawnOffset;
            spawnPosition += Vector3.forward * 3f;

            Stack newStack = Instantiate(stackPrefab, newSpawnPosition, Quaternion.identity, transform);
        }

        private void UpdatePlayerPath(Stack stack)
        {
            playerPath[playerPath.Count - 1].position = stack.PlayerMoveTargets[0].position;

            playerPath.AddRange(stack.PlayerMoveTargets);
        }
    }
}