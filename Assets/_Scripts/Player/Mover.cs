using KinematicCharacterController;
using NaughtyAttributes;
using UnityEngine;

namespace Boxfriend.Player
{
    public class Mover : MonoBehaviour, ICharacterController, IClientSideComponent
    {
        [Required("KinematicCharacterMotor component must be assigned")]
        [SerializeField] private KinematicCharacterMotor _characterMotor;

        [Header("Gravity")]
        [SerializeField] private float _gravityForce = 9.81f;
        [SerializeField] private float _maxFallSpeed = 33f;
        private float _currentGravity;

        [SerializeField] private LayerMask _collisionMask;
        [Required("InputProvider component must be assigned")]
        [SerializeField] private InputProvider _inputProvider;

        [Header("Movement Speeds")]
        [SerializeField] private float _walkSpeed;
        [SerializeField] private float _sprintSpeed;
        [SerializeField] private float _rotateSpeed;

        private Vector3 _moveDirection;

        private float _yRotation;
        private bool _sprinting;
        private void Awake () => _characterMotor.CharacterController = this;
        public void ActivateClient ()
        {
            Cursor.lockState = CursorLockMode.Locked;
            _inputProvider.OnMovement += ctx => _moveDirection = ctx;
            _inputProvider.OnRotation += ctx => _yRotation = ctx;
            _inputProvider.OnSprint += ctx => _sprinting = ctx;

            _characterMotor.enabled = true;
        }

        public void AfterCharacterUpdate (float deltaTime) { }

        public void BeforeCharacterUpdate (float deltaTime) { }

        public bool IsColliderValidForCollisions (Collider coll) => (_collisionMask & (1 << coll.gameObject.layer)) != 0;

        public void OnDiscreteCollisionDetected (Collider hitCollider) { }

        public void OnGroundHit (Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport) { }

        public void OnMovementHit (Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport) { }

        public void PostGroundingUpdate (float deltaTime) { }

        public void ProcessHitStabilityReport (Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, Vector3 atCharacterPosition, Quaternion atCharacterRotation, ref HitStabilityReport hitStabilityReport) { }

        public void UpdateRotation (ref Quaternion currentRotation, float deltaTime) => currentRotation *= Quaternion.AngleAxis(_yRotation * _rotateSpeed, Vector3.up);

        public void UpdateVelocity (ref Vector3 currentVelocity, float deltaTime)
        {
            var gravityForce = Mathf.Min(_currentGravity + (_gravityForce * deltaTime), _maxFallSpeed);
            _currentGravity = (_characterMotor.GroundingStatus.IsStableOnGround ? 0 : gravityForce);
            var gravity = _currentGravity * Vector3.down;

            var speed = _sprinting ? _sprintSpeed : _walkSpeed;
            var moveDirection = transform.TransformDirection(_moveDirection * speed);

            currentVelocity = moveDirection + gravity;
        }

        private void Reset ()
        {
            _characterMotor = transform.root.GetComponentInChildren<KinematicCharacterMotor>();
            _inputProvider = transform.root.GetComponentInChildren<InputProvider>();

            _walkSpeed = 2f;
            _sprintSpeed = 3f;
            _rotateSpeed = 1f;

            _collisionMask = ~0;


        }

    }
}
