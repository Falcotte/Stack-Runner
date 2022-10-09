using UnityEngine;
using DG.Tweening;

namespace StackRunner.StackSystem
{
    public class StackVisual : MonoBehaviour
    {
        [SerializeField] private MeshRenderer[] meshRenderers;

        [SerializeField] private float perfectPlacementAnimationColorAmount;
        [SerializeField] private float perfectPlacementAnimationScaleAmount;
        [SerializeField] private float perfectPlacementAnimationDuration;

        private Color materialDefaultColor;

        public void SetMaterial(Material material)
        {
            foreach(var meshRenderer in meshRenderers)
            {
                meshRenderer.material = material;
                materialDefaultColor = meshRenderer.material.color;
            }
        }

        public void PlayPerfectPlacementAnimation()
        {
            meshRenderers[0].material.DOColor(new Color(materialDefaultColor.r + perfectPlacementAnimationColorAmount, materialDefaultColor.g + perfectPlacementAnimationColorAmount, materialDefaultColor.b + perfectPlacementAnimationColorAmount), perfectPlacementAnimationDuration / 2f).OnComplete(() =>
            {
                meshRenderers[0].material.DOColor(materialDefaultColor, perfectPlacementAnimationDuration / 2f);
            });

            meshRenderers[0].transform.DOPunchScale(Vector3.right * perfectPlacementAnimationScaleAmount, perfectPlacementAnimationDuration, 1, 1).SetEase(Ease.OutBack);
        }
    }
}
