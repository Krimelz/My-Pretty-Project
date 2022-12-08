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
	private float minAngle = -87f;
	[SerializeField]
	private float maxAngle = 87f;

	private Vector3 movementDirection;
	private float rotX;
	private float rotY;
	private Rigidbody rbody;
	[SerializeField]
	private Transform origin;
	[SerializeField]
	private float radius;
	[SerializeField]
	private float maxDistance;
	[SerializeField]
	private LayerMask layerMask;

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

	private void Start()
	{
		rotX = 0f;
		rotY = 0f;

		rbody = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
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
		//transform.Translate(movementDirection * Time.deltaTime * movementSpeed, Space.Self);

		rbody.AddRelativeForce(movementDirection, ForceMode.VelocityChange);
	}

	private void Jump()
	{
		Debug.Log(layerMask.value);
		if (Physics.SphereCast(origin.position, radius, -transform.up, out RaycastHit hit, maxDistance, layerMask.value))
		{
			Debug.Log(2);
			rbody.AddRelativeForce(Vector3.up * jumpForce, ForceMode.Impulse);
		}
	}

	private void Look(Vector2 delta)
	{
		delta *= Time.deltaTime * 10f;

		rotX -= delta.y;
		rotY += delta.x;

		rotX = Mathf.Clamp(rotX, minAngle, maxAngle);

		transform.localRotation = Quaternion.Euler(0f, rotY, 0f);
		playerCamera.transform.localRotation = Quaternion.Euler(rotX, 0f, 0f);
	}

	private void Fire()
	{

	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawRay(origin.position, -transform.up * maxDistance);
		Gizmos.DrawWireSphere(origin.position, radius);
	}
}
