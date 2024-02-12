using Minimalist.Interfaces;
using Minimilist.Pet.Ability;
using System;
using System.Collections;
using System.Collections.Generic;
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

        [field: SerializeField] public FollowStates FollowState { get; private set; }
        [field: SerializeField] public DetectionStates DetectionState { get; private set; }

        private void Start()
        {
            DetectionState = DetectionStates.Idle;
            if(playerTrans == null)
            {
                playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
                if(playerTrans == null)
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
                    if (Vector2.Distance(transform.position, playerTrans.position) > 1.75f)
                        Follow();
                    break;
            }

            void Follow()
            {
                var dir = playerTrans.position.x - transform.position.x;
                var xScale = Mathf.Abs(transform.localScale.x);
                transform.localScale = new Vector2(dir < 0 ? -xScale: xScale, transform.localScale.y);

                var targetPos = Vector2.MoveTowards(transform.position, playerTrans.position, Time.deltaTime * followSpeed);
                targetPos.y = transform.position.y;
                transform.position = targetPos;
            }
        }

        private void HandleDetection()
        {
            if (detectionForGrowling.HasDetected)
            {
                DetectionState = DetectionStates.Alert;
                Debug.Log("Enemy Is Close!");

                if (detectionForBarking.HasDetected)
                {
                    DetectionState = DetectionStates.Spotted;
                    Debug.Log("Enemy Spotted!");
                }
            }
            else
                DetectionState = DetectionStates.Idle;
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