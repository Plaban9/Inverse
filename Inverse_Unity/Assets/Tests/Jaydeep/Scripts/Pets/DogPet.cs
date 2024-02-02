using Minimilist.Pet.Ability;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minimilist.Pet
{
    public class DogPet : MonoBehaviour
    {
        [SerializeField] private PetDetect detectionForGrowling;
        [SerializeField] private PetDetect detectionForBarking;

        private void Update()
        {
            if (detectionForGrowling.HasDetected)
            {
                Debug.Log("Enemy Spotted!");

                if(detectionForBarking.HasDetected)
                {
                    Debug.Log("Enemy Is Close!");
                }
            }
        }
    }
}