using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NightRacer.PlayerInput
{
    public class InputReader : MonoBehaviour
    {
        [SerializeField] private InputActionReference moveAction;
        [SerializeField] private InputActionReference pauseAction;

        public event Action<int> OnLaneChange;
        public event Action OnPause;

        private int lastSign;

        private void OnEnable()
        {
            if (moveAction != null && moveAction.action != null)
            {
                moveAction.action.performed += HandleMove;
                moveAction.action.canceled += HandleMove;
                moveAction.action.Enable();
            }

            if (pauseAction != null && pauseAction.action != null)
            {
                pauseAction.action.performed += HandlePause;
                pauseAction.action.Enable();
            }
        }

        private void OnDisable()
        {
            if (moveAction != null && moveAction.action != null)
            {
                moveAction.action.performed -= HandleMove;
                moveAction.action.canceled -= HandleMove;
                moveAction.action.Disable();
            }

            if (pauseAction != null && pauseAction.action != null)
            {
                pauseAction.action.performed -= HandlePause;
                pauseAction.action.Disable();
            }

            lastSign = 0;
        }

        private void HandleMove(InputAction.CallbackContext context)
        {
            float value = context.ReadValue<float>();
            int sign = value > 0.5f ? 1 : (value < -0.5f ? -1 : 0);

            if (sign != 0 && sign != lastSign)
            {
                OnLaneChange?.Invoke(sign);
            }

            lastSign = sign;
        }

        private void HandlePause(InputAction.CallbackContext context)
        {
            OnPause?.Invoke();
        }
    }
}

