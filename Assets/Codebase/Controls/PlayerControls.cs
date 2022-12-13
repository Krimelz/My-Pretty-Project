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
	private float jumpHeight;
	[Header("Look")]
	[SerializeField]
	private Camera playerCamera;
	[SerializeField]
	private float minAngle = -87f;
	[SerializeField]
	private float maxAngle = 87f;

	private CharacterController character;
	private Vector3 movementDirection;
	private Vector3 velocity;
	private float rotationX;
	private float rotationY;

	private void OnEnable()
	{
		move.action.Enable();
		move.action.performed += OnMovePreformed;

		jump.action.Enable();
		//jump.action.performed += OnJumpPerformed;

		look.action.Enable();
		look.action.performed += OnLookPerformed;

		fire.action.Enable();
		fire.action.performed += OnFirePerformed;
	}

	private void OnDisable()
	{
		move.action.performed -= OnMovePreformed;
		move.action.Disable();

		//jump.action.performed -= OnJumpPerformed;
		jump.action.Disable();

		look.action.performed -= OnLookPerformed;
		look.action.Disable();

		fire.action.performed -= OnFirePerformed;
		fire.action.Disable();
	}

	private void Awake()
	{
		character = GetComponent<CharacterController>();
	}

	private void Start()
	{
		rotationX = 0f;
		rotationY = 0f;
	}

	private void Update()
	{
		if (move.action.IsPressed())
		{
			Move();
		}

		if (jump.action.IsPressed())
		{
			Jump();
		}

		if (character.isGrounded && velocity.y < 0)
		{
			velocity.y = 0f;
		}

		Gravity();
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
		Vector3 forward = transform.TransformDirection(Vector3.forward);
		Vector3 right = transform.TransformDirection(Vector3.right);
		Vector3 motion = (right * movementDirection.x) + (forward * movementDirection.z);
		motion *= movementSpeed * Time.deltaTime;

		character.Move(motion);
	}

	private void Jump()
	{
		if (character.isGrounded)
		{
			velocity.y += Mathf.Sqrt(-jumpHeight * Physics.gravity.y);
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
		Debug.Log("Fire!");
	}

	private void Gravity()
	{
		velocity.y += Physics.gravity.y * Time.deltaTime;
		character.Move(velocity * Time.deltaTime);
	}
}
