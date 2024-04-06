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
        private readonly int AttackHash = Animator.StringToHash("Attack");

        private Animator animator;
        private Enemy enemy;
        private EnemyAttack attack;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            enemy = GetComponent<Enemy>();
            attack = GetComponentInChildren<EnemyAttack>();
        }

        private void OnEnable()
        {
            attack.OnAttack += OnAttack;
        }

        private void OnDisable()
        {
            attack.OnAttack -= OnAttack;
        }

        private void OnAttack()
        {
            animator.SetTrigger(AttackHash);    
        }

        private void Update()
        {
            animator.SetBool(ChaseHash, enemy.IsChasing && enemy.IsEnemyMoving);
            animator.SetBool(AlertHash, (enemy.IsAlert && enemy.IsEnemyMoving) || enemy.IsPatrolling);
        }
    }
}