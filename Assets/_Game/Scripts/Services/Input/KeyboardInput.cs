using System;
using UnityEngine;

namespace _Game.Scripts.Services.Input
{
    public class KeyboardInput : MonoBehaviour, IInputService
    {
        private const string MOVEMENT_ANIMATION_VARIABLE = "movement";
        
        [SerializeField] private float walkSpeed = 2f;
        
        [SerializeField] private float rotationSpeed = 10f;

        [SerializeField] private float runSpeed = 5f;
        
        [SerializeField] private float runChangingScale = 5f;
        
        private float _currentMoveSpeed;
        
        private CharacterController _characterController;

        private Animator _characterAnimator;

        private Vector2 _playerDirection;

        public Transform playerModel;

        private void OnValidate()
        {
            _characterController ??= GetComponent<CharacterController>();
            _characterAnimator ??= GetComponent<Animator>();
        }

        private void Start()
        {
            _currentMoveSpeed = walkSpeed;
        }

        private void Update()
        {
            Move();
            
        }

        private void IncreaseRunSpeed()
        {
            if (_currentMoveSpeed < runSpeed)
            {
                _currentMoveSpeed += Time.deltaTime * runChangingScale;
            }
        }
        private void DecreaseRunSpeed()
        {
            if (_currentMoveSpeed > walkSpeed)
            {
                _currentMoveSpeed -= Time.deltaTime * runChangingScale;
            }
        }
        
        private void Run()
        {
            if (UnityEngine.Input.GetKey(KeyCode.LeftShift))
            {
                IncreaseRunSpeed();
            }
            else
            {
                DecreaseRunSpeed();
            }
        }

        private void Move()
        {
            _playerDirection.y = UnityEngine.Input.GetAxis("Vertical");
            _playerDirection.x = UnityEngine.Input.GetAxis("Horizontal");
            Rotate();
            float animValue = 0;
            if (_playerDirection != Vector2.zero)
            {
                Run();
                _characterController.Move(new Vector3(_playerDirection.x, 0, _playerDirection.y) * (Time.deltaTime * _currentMoveSpeed)); 
                animValue = Math.Max(Math.Abs(_playerDirection.x), Math.Abs(_playerDirection.y)) * _currentMoveSpeed;
                _characterAnimator.SetFloat(MOVEMENT_ANIMATION_VARIABLE, animValue);
            }
        }

        private void Rotate()
        {
            if (new Vector3(_playerDirection.x,0,_playerDirection.y) != Vector3.zero) {
                playerModel.transform.rotation = Quaternion.Slerp(
                    playerModel.transform.rotation,
                    Quaternion.LookRotation(new Vector3(_playerDirection.x,0,_playerDirection.y)),
                    Time.deltaTime * rotationSpeed
                );
            }
        }

        public Vector2 Axis { get; }
        public bool IsCrouching { get; set; }
    }
}

