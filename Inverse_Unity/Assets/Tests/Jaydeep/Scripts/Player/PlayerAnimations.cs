using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minimalist.Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimations : MonoBehaviour
    {
        private readonly int VelocityHash = Animator.StringToHash("Velocity");
        private readonly int DeathHash = Animator.StringToHash("Death");
        private readonly int GroundedHash = Animator.StringToHash("IsGrounded");

        private Animator animator;
        private PlayerMovements movements;
        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            movements = GetComponent<PlayerMovements>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            float speed = Mathf.Abs(movements.Velocity.x);
            animator.SetFloat(VelocityHash, speed);
            animator.SetBool(GroundedHash, movements.IsGrounded);

            if (movements.Velocity.x != 0)
                spriteRenderer.flipX = movements.Velocity.x < 0;
        }
    }
}