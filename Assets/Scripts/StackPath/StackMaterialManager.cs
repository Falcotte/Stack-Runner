using UnityEngine;

namespace StackRunner.StackSystem
{
    public class StackMaterialManager : MonoBehaviour
    {
        [SerializeField] private StackMaterialPreset materialPreset;

        private void Start()
        {
            materialPreset.ResetIndex();
        }

        public Material GetMaterial()
        {
            return materialPreset.GetNextMaterial();
        }
    }
}
