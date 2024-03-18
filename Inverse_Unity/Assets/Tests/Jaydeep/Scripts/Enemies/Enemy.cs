using Minimilist.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Minimalist.Interfaces;
using Minimalist.Level;
using Minimalist.Manager;
using DG.Tweening;
using Minimalist.Player;

namespace Minimilist.Enemies
{
    public class Enemy : MonoBehaviour, ILevelListener<LevelType>
    {
        [SerializeField] private Transform player;
        [SerializeField] private PatrolStates state;
        [SerializeField] private float chaseSpeed = 2;

        [Header("Colliders")]
        [SerializeField] private Collider2D idleCollider;
        [SerializeField] private Collider2D alertCollider;
        [SerializeField] private Collider2D chaseCollider;

        [Header("Detection")]
        [SerializeField] private EnemyDetection enemyDetection;

        private ObjectMovement patrolling;
        [SerializeField] private Vector2 lastPlayerPos = Vector2.zero;
        private Rigidbody2D rb;
        private Animator animator;
        [SerializeField] private bool _darkState = false;

        public bool IsChasing { get => state == PatrolStates.Chase; }
        public bool IsAlert { get => state == PatrolStates.Alert; }
        public bool IsPatrolling { get => patrolling.IsMoving; }

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
            animator = GetComponent<Animator>();
            if (enemyDetection == null)
                enemyDetection = GetComponentInChildren<EnemyDetection>();
            patrolling.StopMoving();
        }

        private void OnDestroy()
        {
            LevelManager.Instance.RealmManager.RemoveListener(this);
        }

        private void Start()
        {
            state = PatrolStates.Idle;
            LevelManager.Instance.RealmManager.AddListener(this);
            OnNotify(LevelManager.Instance.RealmManager.GetCurrentLevelType());
        }

        private void Update()
        {
            if (!_darkState)
                state = PatrolStates.Statue;
            else if (enemyDetection.HasDetected && state != PatrolStates.Chase)
                state = PatrolStates.Chase;
            else if(!enemyDetection.HasDetected && state == PatrolStates.Chase)
                state = PatrolStates.Alert;

            HandleColliders();
        }

        private void FixedUpdate()
        {
            switch (state)
            {
                case PatrolStates.Idle:     HandleIdleState();      break;
                case PatrolStates.Chase:    HandleChaseState();     break;
                case PatrolStates.Alert:    HandleAlertState();     break;
                case PatrolStates.Patrol:   HandlePatrolState();    break;
                case PatrolStates.Statue:   HandleStatueState();    break;
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

                Vector2 targetPos = new(dir.x < 0 ? -chaseSpeed : chaseSpeed, rb.velocity.y);
                rb.velocity = Vector2.Lerp(rb.velocity, targetPos, Time.fixedDeltaTime * chaseSpeed);
                lastPlayerPos = player.position;
                lastPlayerPos.y = transform.position.y;
            }

            void HandleAlertState()
            {
                var dir = (Vector3)lastPlayerPos - transform.position;
                dir.Normalize();

                Vector2 targetPos = new (dir.x < 0 ? -chaseSpeed : chaseSpeed, rb.velocity.y);
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

            void HandleStatueState()
            {
                patrolling.StopMoving();
            }
        }

        private void HandleColliders()
        {
            switch (state)
            {
                case PatrolStates.Idle:
                    idleCollider.enabled = true;
                    alertCollider.enabled = false;
                    chaseCollider.enabled = false;
                    break;
                case PatrolStates.Chase:
                    idleCollider.enabled = false;
                    alertCollider.enabled = false;
                    chaseCollider.enabled = true;
                    break;

                case PatrolStates.Patrol:
                case PatrolStates.Alert:
                    idleCollider.enabled = false;
                    alertCollider.enabled = true;
                    chaseCollider.enabled = false;
                    break;
            }
        }


        public void OnNotify(LevelType type)
        {
            _darkState = type == LevelType.Dark;
            Debug.Log($"Enemy state {type}");
            switch (type)
            {
                case LevelType.Dark:
                    ActivateDarkState();
                    break;
                case LevelType.Light:
                    ActivateLightState();
                    break;
            }
        }

        private void ActivateLightState()
        {
            _darkState = false;
            //rb.isKinematic = true;
            rb.velocity = Vector2.zero;
            rb.mass = 100 * 100;
            animator.speed = 0;
            gameObject.layer = 9;
        }

        private void ActivateDarkState()
        {
            _darkState = true;
            //rb.isKinematic = false;
            state = PatrolStates.Idle;
            rb.mass = 10;
            animator.speed = 1;
            gameObject.layer = 0;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.transform.TryGetComponent<PlayerMovements>(out var playerMovements))
            {
                Debug.Log("GAME OVER");
            }
        }
    }
}