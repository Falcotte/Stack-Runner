using UnityEngine;
using StackRunner.InputSystem;

namespace StackRunner.StackSystem
{
    public class StackPath : MonoBehaviour
    {
        [SerializeField] private Stack stackPrefab;

        [SerializeField] private float stackSpawnOffset;
        private Vector3 spawnPosition;

        private void OnEnable()
        {
            InputController.OnTouchDown += SpawnNewStack;
        }

        private void OnDisable()
        {
            InputController.OnTouchDown -= SpawnNewStack;
        }

        private void Start()
        {
            spawnPosition = Vector3.forward * 12f;
        }

        private void SpawnNewStack()
        {
            Vector3 newSpawnPosition = spawnPosition + Mathf.Sign(Random.Range(-1f, 1f)) * Vector3.right * stackSpawnOffset;

            Stack newStack = Instantiate(stackPrefab, newSpawnPosition, Quaternion.identity, transform);
            spawnPosition += Vector3.forward * 3f;
        }
    }
}