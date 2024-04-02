using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minimalist.Enemies
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimations : MonoBehaviour
    {
        private readonly int ChaseHash = Animator.StringToHash("Chase");
        private readonly int AlertHash = Animator.StringToHash("Alert");

        private Animator animator;
        private Enemy enemy;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            enemy = GetComponent<Enemy>();
        }

        private void Update()
        {
            animator.SetBool(ChaseHash, enemy.IsChasing);
            animator.SetBool(AlertHash, enemy.IsAlert || enemy.IsPatrolling);
        }
    }
}