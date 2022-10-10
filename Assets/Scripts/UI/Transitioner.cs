using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

namespace StackRunner.UI
{
    public class Transitioner : MonoBehaviour
    {
        [SerializeField] private Image transitionerImage;

        public static UnityAction OnTransitionerOpen;
        public static UnityAction OnTransitionerClose;

        public void OpenImmediately(Color? overrideColor = null)
        {
            var color = overrideColor ?? transitionerImage.color;
            color.a = 1;
            transitionerImage.color = color;
            gameObject.SetActive(true);
            OnTransitionerOpen?.Invoke();
        }

        public void CloseImmediately()
        {
            var color = transitionerImage.color;
            color.a = 0;
            transitionerImage.color = color;
            OnTransitionerClose?.Invoke();
            gameObject.SetActive(false);
        }

        public void OpenTransitioner(float animationTime, float delayTime = 0f, Color? overrideColor = null)
        {
            var color = overrideColor ?? transitionerImage.color;
            transitionerImage.color = color;
            gameObject.SetActive(true);
            transitionerImage.DOFade(1f, animationTime).SetEase(Ease.Linear).SetDelay(delayTime).OnComplete(() =>
            {
                OnTransitionerOpen?.Invoke();
            });
        }

        public void CloseTransitioner(float animationTime, float delayTime = 0f)
        {
            transitionerImage.DOFade(0f, animationTime).SetEase(Ease.Linear).SetDelay(delayTime).OnComplete(() =>
            {
                OnTransitionerClose?.Invoke();
                gameObject.SetActive(false);
            });
        }
    }
}
