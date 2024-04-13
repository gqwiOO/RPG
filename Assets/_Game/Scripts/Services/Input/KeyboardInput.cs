using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.Scripts.Services.Input
{
    public class KeyboardInput : MonoBehaviour, IInputService
    {
        private const string MOVEMENT_ANIMATION_VARIABLE = "movement";
        
        [SerializeField] private float walkSpeed = 2f;

        public Transform playerModel;

        [SerializeField] private float rotationSpeed = 10f;

        [SerializeField] private float runSpeed = 4f;
        
        private CharacterController _characterController;
        private Animator _characterAnimator;

        private Vector2 _playerDirection;

        private float _currentMoveSpeed;

        private bool _isRunning = false;

        private void OnValidate()
        {
            _characterController ??= GetComponent<CharacterController>();
            _characterAnimator ??= GetComponent<Animator>();
        }

        void Update()
        {
            Move();
            Run();
        }

        private void Run()
        {
            if (UnityEngine.Input.GetKey(KeyCode.LeftShift))
            {
                _isRunning = true;
                _currentMoveSpeed = runSpeed;
            }
            else
            {
                _currentMoveSpeed = walkSpeed;
                _isRunning = false;
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
                _characterController.Move(new Vector3(_playerDirection.x, 0, _playerDirection.y) * (Time.deltaTime * _currentMoveSpeed)); 
                animValue = Math.Max(Math.Abs(_playerDirection.x), Math.Abs(_playerDirection.y));
                if (_isRunning) animValue += 1f;
            }
            _characterAnimator.SetFloat(MOVEMENT_ANIMATION_VARIABLE, animValue);
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

