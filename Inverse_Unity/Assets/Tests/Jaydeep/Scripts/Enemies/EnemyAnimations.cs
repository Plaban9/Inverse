using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minimilist.Enemies
{
    public class EnemyAnimations : MonoBehaviour
    {
        private readonly int ChaseHash = Animator.StringToHash("Chase");
        private readonly int AlertHash = Animator.StringToHash("Alert");

        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }
    }
}