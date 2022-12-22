using Codebase.Guns;
using Codebase.Services;
using UnityEngine;
using Zenject;

namespace Codebase.Components
{
    public class Player : MonoBehaviour
    {
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
        [Header("Fire")]
        [SerializeField]
        private Gun gun;

        private CharacterController _character;
        private Vector3 _velocity;
        private float _rotationX;
        private float _rotationY;
        private IInputService _inputService;

        [Inject]
        private void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }
        
        private void Start()
        {
            _character = GetComponent<CharacterController>();
            
            _rotationX = 0f;
            _rotationY = 0f;

            _inputService.OnMove += Move;
            _inputService.OnJump += Jump;
            _inputService.OnLook += Look;
            _inputService.OnFire += Fire;
        }
        
        private void Update()
        {
            if (_character.isGrounded && _velocity.y < 0)
            {
                _velocity.y = 0f;
            }
            
            Gravity();
        }
        
        private void Move(Vector2 direction)
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);
            Vector3 motion = (right * direction.x) + (forward * direction.y);
            motion *= movementSpeed * Time.deltaTime;

            _character.Move(motion);
        }

        private void Jump()
        {
            if (_character.isGrounded)
            {
                _velocity.y += Mathf.Sqrt(-jumpHeight * Physics.gravity.y);
            }
        }

        private void Look(Vector2 delta)
        {
            delta *= Time.deltaTime * 10f;

            _rotationX -= delta.y;
            _rotationY += delta.x;

            _rotationX = Mathf.Clamp(_rotationX, minAngle, maxAngle);

            transform.localRotation = Quaternion.Euler(0f, _rotationY, 0f);
            playerCamera.transform.localRotation = Quaternion.Euler(_rotationX, 0f, 0f);
        }

        private void Fire()
        {
            gun.Shoot();
        }

        private void Gravity()
        {
            _velocity.y += Physics.gravity.y * Time.deltaTime;
            _character.Move(_velocity * Time.deltaTime);
        }

        private void OnDestroy()
        {
            _inputService.OnMove -= Move;
            _inputService.OnJump -= Jump;
            _inputService.OnLook -= Look;
            _inputService.OnFire -= Fire;
        }
    }
}