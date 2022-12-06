using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField]
    private InputActionProperty move;
    [SerializeField]
    private InputActionProperty jump;
    [SerializeField]
    private InputActionProperty look;
	[SerializeField]
	private InputActionProperty fire;
	[Header("Move")]
	[SerializeField]
	private float movementSpeed;
	[Header("Jump")]
	[SerializeField]
	private float jumpForce;
	[Header("Look")]
	[SerializeField]
	private Camera playerCamera;
	[SerializeField]
	[Range(0f, 90f)]
	private float minAngle;
	[SerializeField]
	[Range(0f, 90f)]
	private float maxAngle;

	private Vector3 movementDirection;

	private void OnEnable()
	{
		move.action.Enable();
		move.action.performed += OnMovePreformed;

		jump.action.Enable();
		jump.action.performed += OnJumpPerformed;

		look.action.Enable();
		look.action.performed += OnLookPerformed;

		fire.action.Enable();
		fire.action.performed += OnFirePerformed;
	}

	private void OnDisable()
	{
		move.action.performed -= OnMovePreformed;
		move.action.Disable();

		jump.action.performed -= OnJumpPerformed;
		jump.action.Disable();

		look.action.performed -= OnLookPerformed;
		look.action.Disable();
	}

	private void Update()
	{
		if (move.action.IsPressed())
		{
			Move();
		}
	}

	private void OnMovePreformed(InputAction.CallbackContext context)
	{
		var value = context.ReadValue<Vector2>();
		movementDirection = new Vector3(value.x, 0f, value.y);
	}

	private void OnJumpPerformed(InputAction.CallbackContext context)
	{
		Jump();
	}

	private void OnLookPerformed(InputAction.CallbackContext context)
	{
		Look(context.ReadValue<Vector2>());
	}

	private void OnFirePerformed(InputAction.CallbackContext context)
	{
		Fire();
	}

	private void Move()
	{
		transform.Translate(movementDirection * Time.deltaTime * movementSpeed, Space.Self);
	}

	private void Jump()
	{
		
	}

	private void Look(Vector2 delta)
	{
		delta *= Time.deltaTime * 10f;

		var xRot = playerCamera.transform.localRotation.eulerAngles.x;
		var yRot = transform.localRotation.eulerAngles.y;

		var newXRot = xRot - delta.y;
		var newYRot = yRot + delta.x;

		playerCamera.transform.localRotation = Quaternion.Euler(newXRot, 0f, 0f);
		transform.localRotation = Quaternion.Euler(0f, newYRot, 0f);
	}

	private void Fire()
	{

	}
}
