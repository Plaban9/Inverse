using Minimalist.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Minimalist.Interfaces;
using Minimalist.Level;
using Minimalist.Manager;
using DG.Tweening;
using Minimalist.Player;

namespace Minimalist.Enemies
{
    public class Enemy : MonoBehaviour, ILevelListener<LevelType>
    {
        [SerializeField] private Transform player;
        [SerializeField] protected PatrolStates state;
        [SerializeField] private float chaseSpeed = 2;
        [SerializeField] private float attackInterval = 1f;

        [Header("Colliders")]
        [SerializeField] private Collider2D idleCollider;
        [SerializeField] private Collider2D alertCollider;
        [SerializeField] private Collider2D chaseCollider;

        [Header("Detection")]
        [SerializeField] private EnemyDetection enemyDetection;

        [Header("EnemyRealm")]
        [SerializeField] protected bool isRealmIndependentEnemy;
        [SerializeField] protected bool isLightRealmEnemy;

        [SerializeField] private Vector2 lastPlayerPos = Vector2.zero;
        [SerializeField] protected bool _canMove = false;

        private ObjectMovement patrolling;
        protected EnemyAttack enemyAttack;
        protected Rigidbody2D rb;
        protected Animator animator;

        public bool IsChasing { get => state == PatrolStates.Chase; }
        public bool IsAlert { get => state == PatrolStates.Alert; }
        public bool IsPatrolling { get => patrolling.IsMoving; }

        protected enum PatrolStates
        {
            Idle,
            Chase,
            Alert,
            Statue,
            Patrol,
        }

        private void Awake()
        {
            enemyAttack = GetComponentInChildren<EnemyAttack>();
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
            enemyAttack.attackInterval = attackInterval;
            LevelManager.Instance.RealmManager.AddListener(this);
            OnNotify(LevelManager.Instance.RealmManager.GetCurrentLevelType());
        }

        private void Update()
        {
            if (!_canMove)
                state = PatrolStates.Statue;
            else if (enemyDetection.HasDetected && state != PatrolStates.Chase)
                state = PatrolStates.Chase;
            else if (!enemyDetection.HasDetected && state == PatrolStates.Chase)
                state = PatrolStates.Alert;

            HandleColliders();
        }

        private void FixedUpdate()
        {
            switch (state)
            {
                case PatrolStates.Idle: HandleIdleState(); break;
                case PatrolStates.Chase: HandleChaseState(); break;
                case PatrolStates.Alert: HandleAlertState(); break;
                case PatrolStates.Patrol: HandlePatrolState(); break;
                case PatrolStates.Statue: HandleStatueState(); break;
                default: Debug.LogError($"Something went wrong! {state} is not implemented!"); break;
            }
        }

        protected virtual void HandleIdleState()
        {
            patrolling.StopMoving();
            StartCoroutine(ChangeState(PatrolStates.Patrol, 10));
        }

        protected virtual void HandleChaseState()
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

        protected virtual void HandleAlertState()
        {
            var dir = (Vector3)lastPlayerPos - transform.position;
            dir.Normalize();

            Vector2 targetPos = new(dir.x < 0 ? -chaseSpeed : chaseSpeed, rb.velocity.y);
            if (Mathf.Abs(lastPlayerPos.x - transform.position.x) > 0)
                rb.velocity = Vector2.Lerp(rb.velocity, targetPos, Time.fixedDeltaTime * chaseSpeed / 2);
            else
                rb.velocity = Vector2.zero;

            StartCoroutine(ChangeState(PatrolStates.Patrol, 5f));
        }

        protected virtual void HandlePatrolState()
        {
            patrolling.StartMoving();
            StartCoroutine(ChangeState(PatrolStates.Idle, 20));
        }

        protected IEnumerator ChangeState(PatrolStates newState, float changeTime)
        {
            yield return new WaitForSeconds(changeTime);
            StopAllCoroutines();
            state = newState;
        }

        protected virtual void HandleStatueState()
        {
            patrolling.StopMoving();
        }

        private void HandleColliders()
        {
            switch (state)
            {
                case PatrolStates.Idle:
                    alertCollider.enabled = false;
                    chaseCollider.enabled = false;
                    idleCollider.enabled = true;
                    break;
                case PatrolStates.Chase:
                    idleCollider.enabled = false;
                    alertCollider.enabled = false;
                    chaseCollider.enabled = true;
                    break;

                case PatrolStates.Patrol:
                case PatrolStates.Alert:
                    idleCollider.enabled = false;
                    chaseCollider.enabled = false;
                    alertCollider.enabled = true;
                    break;
            }
        }


        public void OnNotify(LevelType type)
        {
            _canMove = type == LevelType.Dark;
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

        protected virtual void ActivateLightState()
        {
            _canMove = false;
            rb.velocity = Vector2.zero;
            rb.gravityScale = isRealmIndependentEnemy ? 1 : isLightRealmEnemy ? 1 : 0;
            rb.mass = 100 * 100;
            animator.speed = 0;
            gameObject.layer = 9;
        }

        protected virtual void ActivateDarkState()
        {
            _canMove = true;
            rb.gravityScale = isRealmIndependentEnemy ? 1 : isLightRealmEnemy ? 0 : 1;
            state = PatrolStates.Idle;
            rb.mass = 10;
            animator.speed = 1;
            gameObject.layer = 0;
        }

        protected virtual void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.TryGetComponent<PlayerMovements>(out var playerMovements))
            {
                Debug.Log("GAME OVER");
            }
        }
    }
}