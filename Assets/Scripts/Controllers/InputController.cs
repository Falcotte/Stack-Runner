using UnityEngine;
using UnityEngine.Events;

namespace StackRunner.InputSystem
{
    public class InputController : MonoBehaviour
    {
        public static UnityAction OnTouchDown;

        private void Update()
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
}
