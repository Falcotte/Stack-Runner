using System.Collections.Generic;
using UnityEngine;

namespace StackRunner.StackSystem
{
    [CreateAssetMenu(fileName = "StackMaterialPreset", menuName = "StackRunner/MaterialPreset", order = 1)]
    public class StackMaterialPreset : ScriptableObject
    {
        [SerializeField] private List<Material> materials;

        private int index;

        public Material GetNextMaterial()
        {
            return materials[index++ % materials.Count];
        }
    }
}
