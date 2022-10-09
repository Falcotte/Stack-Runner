using UnityEngine;

namespace StackRunner.StackSystem
{
    public class StackMaterialManager : MonoBehaviour
    {
        [SerializeField] private StackMaterialPreset materialPreset;

        public Material GetMaterial()
        {
            return materialPreset.GetNextMaterial();
        }
    }
}
