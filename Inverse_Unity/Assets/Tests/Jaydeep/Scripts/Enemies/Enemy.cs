using Minimilist.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minimilist.Enemies
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private PatrolStates state;
        [SerializeField] private float chaseSpeed = 2;

        [Header("Patrol")]
        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform endPoint;

        [Header("Detection")]
        [SerializeField] private EnemyDetection enemyDetection;

        private ObjectMovement patrolling;
        [SerializeField] private Vector2 lastPlayerPos = Vector2.zero;
        private Rigidbody2D rb;

        private enum PatrolStates
        {
            Idle,
            Chase,
            Alert,
            Statue,
            Patrol,
        }


        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            patrolling = GetComponent<ObjectMovement>();
            rb = GetComponent<Rigidbody2D>();
            if (enemyDetection == null)
                enemyDetection = GetComponentInChildren<EnemyDetection>();
        }

        private void Start()
        {
            state = PatrolStates.Idle;
        }

        private void Update()
        {
            if (enemyDetection.HasDetected && state != PatrolStates.Chase)
                state = PatrolStates.Chase;
            else if(!enemyDetection.HasDetected && state == PatrolStates.Chase)
                state = PatrolStates.Alert;
        }

        private void FixedUpdate()
        {
            switch (state)
            {
                case PatrolStates.Idle:     HandleIdleState();  break;
                case PatrolStates.Chase:    HandleChaseState(); break;
                case PatrolStates.Alert:    HandleAlertState(); break;
                case PatrolStates.Patrol:   HandlePatrolState(); break;
                case PatrolStates.Statue:
                    // Statue
                    // Dont do anything
                    break;
                default: Debug.LogError($"Something went wrong! {state} is not implemented!"); break;
            }

            void HandleIdleState()
            {
                patrolling.StopMoving();
                StartCoroutine(ChangeState(PatrolStates.Patrol, 10));
            }

            void HandleChaseState()
            {
                StopAllCoroutines();
                patrolling.StopMoving();
                var dir = player.position - transform.position;
                dir.Normalize();

                var xScale = Mathf.Abs(transform.localScale.x);
                transform.localScale = new Vector2(dir.x < 0 ? -xScale : xScale, transform.localScale.y);

                Vector2 targetPos = new Vector2(dir.x < 0 ? -chaseSpeed : chaseSpeed, rb.velocity.y);
                rb.velocity = Vector2.Lerp(rb.velocity, targetPos, Time.fixedDeltaTime * chaseSpeed);
                lastPlayerPos = player.position;
                lastPlayerPos.y = transform.position.y;
            }

            void HandleAlertState()
            {
                var dir = (Vector3)lastPlayerPos - transform.position;
                dir.Normalize();

                Vector2 targetPos = new Vector2(dir.x < 0 ? -chaseSpeed : chaseSpeed, rb.velocity.y);
                if (Mathf.Abs(lastPlayerPos.x - transform.position.x) > 0)
                    rb.velocity = Vector2.Lerp(rb.velocity, targetPos, Time.fixedDeltaTime * chaseSpeed / 2);
                else
                    rb.velocity = Vector2.zero;

                StartCoroutine(ChangeState(PatrolStates.Patrol, 5f));
            }

            void HandlePatrolState()
            {
                patrolling.StartMoving();
                StartCoroutine(ChangeState(PatrolStates.Idle, 20));
            }

            IEnumerator ChangeState(PatrolStates newState, float changeTime)
            {
                yield return new WaitForSeconds(changeTime);
                StopAllCoroutines();
                state = newState;
            }
        }
    }
}