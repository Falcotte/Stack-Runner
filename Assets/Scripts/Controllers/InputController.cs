using UnityEngine;
using UnityEngine.Events;
using StackRunner.UI;

namespace StackRunner.InputSystem
{
    public class InputController : MonoBehaviour
    {
        public static UnityAction OnTouchDown;

        private bool inputEnabled;

        private void OnEnable()
        {
            Transitioner.OnTransitionerOpen += DisableInput;
            Transitioner.OnTransitionerClose += EnableInput;
        }

        private void OnDisable()
        {
            Transitioner.OnTransitionerOpen -= DisableInput;
            Transitioner.OnTransitionerClose -= EnableInput;
        }

        private void Update()
        {
            if(inputEnabled)
            {
#if UNITY_EDITOR || UNITY_STANDALONE
                if(Input.GetMouseButtonDown(0))
                {
                    OnTouchDown?.Invoke();
                }
#elif UNITY_IOS || UNITY_ANDROID
            if(Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if(touch.phase == TouchPhase.Began)
                {
                    OnTouchDown?.Invoke();
                }
            }
#endif
            }
        }

        private void EnableInput()
        {
            inputEnabled = true;
        }

        private void DisableInput()
        {
            inputEnabled = false;
        }
    }
}
