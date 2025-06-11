using System;
using UnityEngine;

namespace NewInputSystem
{
    public class InputReader : MonoBehaviour
    {
        private UserInput _input;

        public event Action JumpPressed;

        public float Direction { get; private set; }

        private void Update()
        {
            Direction = _input.Player.Move.ReadValue<float>();
        }

        private void OnDestroy()
        {
            _input.Disable();
        }

        public void Initialize(UserInput input)
        {
            _input = input;
            _input.Enable();
            _input.Player.Jump.performed += inputAction => JumpPressed?.Invoke();
        }
    }
}