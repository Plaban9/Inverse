using Minimalist.Manager;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Minimalist.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(MyPlayerInput))]
    public class PlayerMovements : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float speed = 5f;
        [SerializeField] private float dashSpeed = 5f;
        [SerializeField] private float dashTime = 1f;
        [SerializeField] private float dashCooldown = 3f;
        [SerializeField] private bool canDash;
        [SerializeField] private bool isDashing;
        [SerializeField] private float currentDashTime;
        [SerializeField] private float currentDashCooldownTime;

        [Header("Jump")]
        [SerializeField] private bool isGrounded;
        [SerializeField] private float jumpHeight = 2f;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float groundCheckRadius = .5f;
        [SerializeField] private int airJumpCap = 1;
        [SerializeField] private float airControl = 0.7f;
        private int _airJumpCount = 0;

        private float fallMultiplier = 2.5f;
        private float lowJumpMultiplier = 2f;

        public bool IsPlayerJumped { get; private set; }


        public Vector2 Velocity { get => _rb.velocity; }
        public bool IsGrounded { get => isGrounded; }

        // Private Fields
        private MyPlayerInput _playerInput;
        private Rigidbody2D _rb;


        private void Awake()
        {
            _playerInput = GetComponent<MyPlayerInput>();
            _rb = GetComponent<Rigidbody2D>();
            currentDashCooldownTime = dashCooldown;
        }

        private void OnEnable()
        {
            _playerInput.OnDie += OnDie;
        }

        private void OnDisable()
        {
            _playerInput.OnDie -= OnDie;
        }

        private void Update()
        {
            HandleMovement();

            HandleJump();
        }

        private void HandleMovement()
        {
            // Move
            float move = _playerInput.MoveVector;
            float finalSpeed = speed;
            if (!isGrounded)
            {
                finalSpeed *= airControl;
            }

            var myVelocity = new Vector2(move * finalSpeed, _rb.velocity.y);
            if (!isDashing)
            {
                currentDashCooldownTime += Time.deltaTime;
                canDash = currentDashCooldownTime > dashCooldown;
                _rb.velocity = myVelocity;
            }
            else
            {
                currentDashTime += Time.deltaTime;
                if(currentDashTime >= dashTime)
                {
                    currentDashTime = 0;
                    currentDashCooldownTime = 0;
                    isDashing = false;
                }
            }

            if(_playerInput.IsDashed && canDash && move != 0)
            {
                isDashing = true;
                var dashDir = myVelocity.x * dashSpeed;
                _rb.velocity = new Vector2(dashDir, _rb.velocity.y);
            }
        }

        private void HandleJump()
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
            IsPlayerJumped = false;

            if (isGrounded)
            {
                _airJumpCount = 0;
            }

            // Jumps
            if ((isGrounded || airJumpCap > _airJumpCount) && _playerInput.IsJumped)
            {
                IsPlayerJumped = true;
                if (!isGrounded) { _airJumpCount++; }
                _rb.velocity = new Vector2(_rb.velocity.x, jumpHeight);
            }

            // Fall
            if (_rb.velocity.y < 0)
            {
                _rb.velocity += (fallMultiplier - 1) * Physics2D.gravity.y * Time.deltaTime * Vector2.up;
            }
            else if (_rb.velocity.y > 0 && !_playerInput.IsJumped)
            {
                _rb.velocity += (lowJumpMultiplier - 1) * Physics2D.gravity.y * Time.deltaTime * Vector2.up;
            }
        }

        private void OnDie()
        {
            SceneManager.Instance.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name, "CrossFade");
        }
    }
}