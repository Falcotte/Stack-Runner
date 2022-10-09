using UnityEngine;
using StackRunner.StackSystem;

namespace StackRunner
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;

        [SerializeField] private AudioClip cutClip;
        [SerializeField] private AudioClip perfectClip;

        [SerializeField] private float perfectPitchIncreaseAmount;
        private float currentPitch = 1f;

        private void OnEnable()
        {
            StackPath.OnPlacement += PlayCutClip;
            StackPath.OnPerfectPlacement += PlayPerfectClip;
        }

        private void OnDisable()
        {
            StackPath.OnPlacement -= PlayCutClip;
            StackPath.OnPerfectPlacement -= PlayPerfectClip;
        }

        private void PlayCutClip()
        {
            currentPitch = 1f;
            audioSource.pitch = currentPitch;

            audioSource.clip = cutClip;
            audioSource.Play();
        }

        private void PlayPerfectClip()
        {
            audioSource.clip = perfectClip;
            audioSource.Play();

            currentPitch += perfectPitchIncreaseAmount;
            currentPitch = Mathf.Clamp(currentPitch, 1f, 3f);

            audioSource.pitch = currentPitch;
        }
    }
}
