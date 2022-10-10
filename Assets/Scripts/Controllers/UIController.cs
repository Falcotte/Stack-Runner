using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using StackRunner.Player;

namespace StackRunner.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI collectedGoldAmount;

        [SerializeField] private Button playAgainButton;
        [SerializeField] private Button tryAgainButton;

        [SerializeField] private Transitioner transitioner;

        private void OnEnable()
        {
            PlayerController.OnPlayerReachFinishStack += ShowPlayAgainButton;
            PlayerController.OnPlayerFall += ShowTryAgainButton;

            PlayerController.OnGoldCollect += UpdateCollectedGoldAmount;
        }

        private void OnDisable()
        {
            PlayerController.OnPlayerReachFinishStack -= ShowPlayAgainButton;
            PlayerController.OnPlayerFall -= ShowTryAgainButton;

            PlayerController.OnGoldCollect -= UpdateCollectedGoldAmount;
        }

        private void Start()
        {
            playAgainButton.enabled = false;
            playAgainButton.transform.localScale = Vector3.zero;

            tryAgainButton.enabled = false;
            tryAgainButton.transform.localScale = Vector3.zero;

            transitioner.OpenImmediately();

            transitioner.CloseTransitioner(1f);
        }

        private void UpdateCollectedGoldAmount(int amount)
        {
            collectedGoldAmount.text = amount.ToString();

            DOTween.Kill(collectedGoldAmount.transform);
            collectedGoldAmount.transform.localScale = Vector3.one;

            collectedGoldAmount.transform.DOPunchScale(Vector3.one * 1.5f, .25f, 1, 1);
        }

        private void ShowPlayAgainButton()
        {
            Sequence showPlayAgainButtonSequence = DOTween.Sequence();
            showPlayAgainButtonSequence.AppendInterval(3.5f);
            showPlayAgainButtonSequence.AppendCallback(() =>
            {
                playAgainButton.transform.DOScale(Vector3.one, .5f).SetEase(Ease.OutBack).OnComplete(() =>
                {
                    playAgainButton.enabled = true;
                });
            });
        }

        public void PlayAgain()
        {
            playAgainButton.enabled = false;

            Sequence playAgainSequence = DOTween.Sequence();
            playAgainSequence.AppendCallback(() =>
            {
                transitioner.OpenTransitioner(1f);
            });
            playAgainSequence.AppendInterval(1f);
            playAgainSequence.AppendCallback(() =>
            {
                DOTween.KillAll();
                SceneManager.LoadScene("GameScene");
            });
        }

        private void ShowTryAgainButton()
        {
            Sequence showTryAgainButtonSequence = DOTween.Sequence();
            showTryAgainButtonSequence.AppendInterval(2f);
            showTryAgainButtonSequence.AppendCallback(() =>
            {
                tryAgainButton.transform.DOScale(Vector3.one, .5f).SetEase(Ease.OutBack).OnComplete(() =>
                {
                    tryAgainButton.enabled = true;
                });
            });
        }

        public void TryAgain()
        {
            tryAgainButton.enabled = false;

            Sequence tryAgainSequence = DOTween.Sequence();
            tryAgainSequence.AppendCallback(() =>
            {
                transitioner.OpenTransitioner(1f);
            });
            tryAgainSequence.AppendInterval(1f);
            tryAgainSequence.AppendCallback(() =>
            {
                DOTween.KillAll();
                SceneManager.LoadScene("GameScene");
            });
        }
    }
}
