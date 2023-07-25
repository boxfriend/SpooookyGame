using KinematicCharacterController;
using UnityEngine;

namespace Boxfriend.Player
{
    [RequireComponent(typeof(KinematicCharacterMotor))]
    public class PlayerMover : MonoBehaviour, ICharacterController
    {
        [SerializeField] private KinematicCharacterMotor _characterMotor;

        [Header("Gravity")]
        [SerializeField] private float _gravityForce = 9.81f;
        [SerializeField] private float _maxFallSpeed = 33f;
        private float _currentGravity;

        [SerializeField] private LayerMask _collisionMask;
        [SerializeField] private PlayerInputProvider _inputProvider;
        [SerializeField] private float _walkSpeed, _sprintSpeed, _maxSpeed;
        [SerializeField] private float _rotateSpeed;

        private Vector3 _moveDirection;
        private Vector3 _groundNormal = Vector3.up;

        private float _yRotation;
        private void Awake ()
        {
            _characterMotor.CharacterController = this;
            Cursor.lockState = CursorLockMode.Locked;
            _inputProvider.OnMovement += ctx => _moveDirection = ctx;
            _inputProvider.OnRotation += ctx => _yRotation = ctx;
        }

        private void Start ()
        {
            
        }

        public void AfterCharacterUpdate (float deltaTime)
        {
            
        }

        public void BeforeCharacterUpdate (float deltaTime)
        {
            
        }

        public bool IsColliderValidForCollisions (Collider coll) => (_collisionMask & (1 << coll.gameObject.layer)) != 0;

        public void OnDiscreteCollisionDetected (Collider hitCollider)
        {
            
        }

        public void OnGroundHit (Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
        {
            _groundNormal = hitNormal;
        }

        public void OnMovementHit (Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
        {
            
        }

        public void PostGroundingUpdate (float deltaTime)
        {
            
        }

        public void ProcessHitStabilityReport (Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, Vector3 atCharacterPosition, Quaternion atCharacterRotation, ref HitStabilityReport hitStabilityReport)
        {
            
        }

        public void UpdateRotation (ref Quaternion currentRotation, float deltaTime)
        {
            currentRotation *= Quaternion.AngleAxis(_yRotation * _rotateSpeed, Vector3.up);
        }

        public void UpdateVelocity (ref Vector3 currentVelocity, float deltaTime)
        {
            var gravityForce = Mathf.Min(_currentGravity + (_gravityForce * deltaTime), _maxFallSpeed);
            _currentGravity = (_characterMotor.GroundingStatus.IsStableOnGround ? 0 : gravityForce);
            var gravity = _currentGravity * Vector3.down;

            var speed = false ? _sprintSpeed : _walkSpeed;
            var moveDirection = transform.TransformDirection(_moveDirection * speed);

            currentVelocity = moveDirection + gravity;
        }

        private void Reset ()
        {
            _characterMotor = GetComponent<KinematicCharacterMotor>();
            _inputProvider = GetComponentInChildren<PlayerInputProvider>();

            _walkSpeed = 2f;
            _sprintSpeed = 3f;
            _maxSpeed = 5f;
            _rotateSpeed = 1f;

            _collisionMask = ~0;
        }

    }
}
