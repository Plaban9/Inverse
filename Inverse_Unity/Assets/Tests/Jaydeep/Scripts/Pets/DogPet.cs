using Minimalist.Interfaces;
using Minimilist.Pet.Ability;
using UnityEngine;

namespace Minimilist.Pet
{
    public class DogPet : MonoBehaviour, IInteractable
    {
        [Header("Follow Settings")]
        [SerializeField] private Transform playerTrans;
        [SerializeField] private float followSpeed = 1f;

        [Header("Detection Settings")]
        [SerializeField] private PetDetect detectionForGrowling;
        [SerializeField] private PetDetect detectionForBarking;

        private float _stopDistance = 1.75f;
        [SerializeField] private FollowStates followState;

        public float Speed
        {
            get
            {
                if (FollowState == FollowStates.Follow)
                {
                    float dist = Vector2.Distance(transform.position, playerTrans.position);
                    return dist > _stopDistance ? 1 : 0;
                }
                else
                    return 0;
            }
        }

        public event System.Action<bool> OnGrowling;
        public event System.Action<bool> OnBarking;
        public event System.Action<bool> OnSit;

        public enum DetectionStates
        {
            Idle,
            Alert,
            Spotted,
        }

        public enum FollowStates
        {
            Sit,
            Follow,
        }

        public FollowStates FollowState
        {
            get => followState;
            private set
            {
                followState = value;
                OnSit?.Invoke(value == FollowStates.Sit);
            }
        }
        [field: SerializeField] public DetectionStates DetectionState { get; private set; }

        private void Start()
        {
            DetectionState = DetectionStates.Idle;
            FollowState = FollowStates.Sit;
            if (playerTrans == null)
            {
                playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
                if (playerTrans == null)
                {
                    Debug.LogError("Player is NOT assigned!");
                }
            }
        }

        private void Update()
        {
            HandleDetection();

            HandleFollowing();
        }

        private void HandleFollowing()
        {
            switch (FollowState)
            {
                case FollowStates.Follow:
                    if (Vector2.Distance(transform.position, playerTrans.position) > _stopDistance && DetectionState == DetectionStates.Idle)
                        Follow();
                    break;
            }

            void Follow()
            {
                var dir = playerTrans.position.x - transform.position.x;
                var xScale = Mathf.Abs(transform.localScale.x);
                transform.localScale = new Vector2(dir < 0 ? -xScale : xScale, transform.localScale.y);

                var targetPos = Vector2.MoveTowards(transform.position, playerTrans.position, Time.deltaTime * followSpeed);
                targetPos.y = transform.position.y;
                transform.position = targetPos;
            }
        }

        private void HandleDetection()
        {

            if (detectionForGrowling.HasDetected && DetectionState != DetectionStates.Alert)
            {
                DetectionState = DetectionStates.Alert;
                //Debug.Log("Enemy Is Close!");
                OnGrowling?.Invoke(detectionForGrowling.HasDetected);
            }
            else if (detectionForBarking.HasDetected && DetectionState != DetectionStates.Spotted)
            {
                DetectionState = DetectionStates.Spotted;
                //Debug.Log("Enemy Spotted!");
                OnBarking?.Invoke(detectionForBarking.HasDetected);
            }
            else
            {
                if (DetectionState != DetectionStates.Idle)
                {
                    OnGrowling?.Invoke(false);
                    OnBarking?.Invoke(false);
                    Debug.Log($"{this.name}: Events for reset are fired");
                }
                DetectionState = DetectionStates.Idle;
            }
        }

        public void SetFollowState(bool canFollow)
        {
            if (canFollow)
            {
                FollowState = FollowStates.Follow;
                var dir = playerTrans.position.x - transform.position.x;
                var xScale = Mathf.Abs(transform.localScale.x);
                transform.localScale = new Vector2(dir < 0 ? -xScale : xScale, transform.localScale.y);
            }
            else
            {
                FollowState = FollowStates.Sit;
            }
        }

        public void Interact()
        {
            bool isFollowing = FollowState == FollowStates.Follow;
            SetFollowState(!isFollowing);
        }
    }
}