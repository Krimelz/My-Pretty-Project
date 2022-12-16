using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Codebase.Services
{
	public class InputService : MonoBehaviour, IInputService
	{
		public event Action<Vector2> OnMove;
		public event Action<Vector2> OnLook;
		public event Action OnJump;
		public event Action OnFire;
		
		[SerializeField]
		private InputActionProperty move;
		[SerializeField]
		private InputActionProperty jump;
		[SerializeField]
		private InputActionProperty look;
		[SerializeField]
		private InputActionProperty fire;

		private void OnEnable()
		{
			move.action.Enable();
			jump.action.Enable();
			look.action.Enable();
			fire.action.Enable();
			
			look.action.performed += OnLookPerformed;
			fire.action.performed += OnFirePerformed;
		}

		private void OnDisable()
		{
			move.action.Disable();
			jump.action.Disable();
			look.action.Disable();
			fire.action.Disable();

			look.action.performed -= OnLookPerformed;
			fire.action.performed -= OnFirePerformed;
		}

		private void Update()
		{
			if (move.action.IsPressed())
			{
				OnMove?.Invoke(move.action.ReadValue<Vector2>());
			}

			if (jump.action.IsPressed())
			{
				OnJump?.Invoke();
			}
		}

		private void OnLookPerformed(InputAction.CallbackContext context)
		{
			OnLook?.Invoke(context.ReadValue<Vector2>());
		}

		private void OnFirePerformed(InputAction.CallbackContext context)
		{
			OnFire?.Invoke();
		}
	}
}
