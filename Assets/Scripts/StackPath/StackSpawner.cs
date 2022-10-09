using UnityEngine;

namespace StackRunner.StackSystem
{
    public class StackSpawner : MonoBehaviour
    {
        [SerializeField] private Stack stackPrefab;

        [SerializeField] private StackPath stackPath;
        [SerializeField] private float stackSpawnOffset;

        [SerializeField] private StackMaterialManager stackMaterialManager;

        private Vector3 spawnPosition;

        private void Start()
        {
            spawnPosition = Vector3.forward * 12f;
        }

        public void SpawnNewStack()
        {
            Vector3 newSpawnPosition = spawnPosition + Vector3.right * (stackPath.PlayerPath[stackPath.PlayerPath.Count - 1].position.x + Mathf.Sign(Random.Range(-1f, 1f)) * stackSpawnOffset);
            spawnPosition += Vector3.forward * 3f;

            Stack newStack = Instantiate(stackPrefab, newSpawnPosition, Quaternion.identity, transform);
            newStack.StackVisualController.SetMaterial(stackMaterialManager.GetMaterial());

            // The first spawned stack doesn't need to be adjusted
            if(stackPath.Stacks.Count > 0)
            {
                newStack.StackWidth = stackPath.Stacks[stackPath.Stacks.Count - 1].StackWidth;
                newStack.StackVisual.transform.localScale = new Vector3(newStack.StackWidth, newStack.StackVisual.transform.localScale.y, newStack.StackVisual.transform.localScale.z);

                newStack.LastStackXPosition = stackPath.Stacks[stackPath.Stacks.Count - 1].transform.position.x;
            }

            stackPath.Stacks.Add(newStack);
        }
    }
}
