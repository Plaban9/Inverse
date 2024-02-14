using Minimalist.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minimilist.Pet
{
    [RequireComponent(typeof(Animator))]
    public class PetAnimations : MonoBehaviour
    {
        private readonly int BarkHash = Animator.StringToHash("Bark");
        private readonly int SitHash = Animator.StringToHash("Sit");
        private readonly int SpeedHash = Animator.StringToHash("Speed");

        private bool isSitting;

        private Animator animator;
        private DogPet pet;

        private void Awake()
        {
            isSitting = false;
            animator = GetComponent<Animator>();
            pet = GetComponent<DogPet>();
        }

        private void OnEnable()
        {
            pet.OnBarking += Pet_OnBarking;
            pet.OnGrowling += Pet_OnGrowling;
            pet.OnSit += Pet_OnSit;
        }

        private void OnDisable()
        {
            pet.OnBarking -= Pet_OnBarking;
            pet.OnGrowling -= Pet_OnGrowling;
            pet.OnSit -= Pet_OnSit;
        }

        private void Start()
        {
            isSitting = pet.FollowState == DogPet.FollowStates.Sit;
        }

        private void Update()
        {
            animator.SetFloat(SpeedHash, pet.Speed);
        }

        private void Pet_OnSit(bool isSitting)
        {
            this.isSitting = isSitting;
            animator.SetBool(SitHash, this.isSitting);
        }

        private void Pet_OnGrowling(bool isGrowling)
        {
            isSitting = false;
            animator.SetBool(SitHash, isSitting);
        }

        private void Pet_OnBarking(bool isBarking)
        {
            isSitting = false;
            animator.SetBool(SitHash, isSitting);
            animator.SetBool(BarkHash, isBarking);
        }
    }
}