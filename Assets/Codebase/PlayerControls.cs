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
	[SerializeField]
	private LayerMask layerMask;
	[SerializeField]
	private Transform groundChecker;
	[SerializeField]
	private float radius;
	[Header("Look")]
	[SerializeField]
	private Camera playerCamera;
	[SerializeField]
	private float minAngle = -87f;
	[SerializeField]
	private float maxAngle = 87f;

	private Rigidbody rbody;
	private Vector3 movementDirection;
	private float rotationX;
	private float rotationY;

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
		rotationX = 0f;
		rotationY = 0f;

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
		var direction = movementDirection * movementSpeed * Time.fixedDeltaTime;
		rbody.AddRelativeForce(direction, ForceMode.VelocityChange);
	}

	private void Jump()
	{
		if (Physics.CheckSphere(groundChecker.position, radius, layerMask.value))
		{
			rbody.AddRelativeForce(Vector3.up * jumpForce, ForceMode.Impulse);
		}
	}

	private void Look(Vector2 delta)
	{
		delta *= Time.deltaTime * 10f;

		rotationX -= delta.y;
		rotationY += delta.x;

		rotationX = Mathf.Clamp(rotationX, minAngle, maxAngle);

		transform.localRotation = Quaternion.Euler(0f, rotationY, 0f);
		playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
	}

	private void Fire()
	{

	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(groundChecker.position, radius);
	}
}
