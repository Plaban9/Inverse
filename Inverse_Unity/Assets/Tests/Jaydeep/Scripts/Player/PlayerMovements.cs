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

        [Header("Jump")]
        [SerializeField] private bool isGrounded;
        [SerializeField] private float jumpHeight = 2f;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float groundCheckRadius = .5f;

        public Vector2 Velocity { get => _rb.velocity; }
        public bool IsGrounded { get => isGrounded; }

        // Private Fields
        private MyPlayerInput _playerInput;
        private Rigidbody2D _rb;

        private void Awake()
        {
            _playerInput = GetComponent<MyPlayerInput>();    
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            float move = _playerInput.MoveVector;

            Vector2 myVelocity = new Vector2(move * speed, _rb.velocity.y);

            //_rb.velocity = myVelocity;
            _rb.velocity = Vector2.Lerp(_rb.velocity, myVelocity, Time.deltaTime * speed);

            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

            if(isGrounded && _playerInput.IsJumped)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, jumpHeight);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Death"))
            {
                
                Debug.Log("Loading Level " + transform.name + "----" + collision.name);
                SceneManager.Instance.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name, "CrossFade");
            }
        }
    }
}