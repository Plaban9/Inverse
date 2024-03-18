using Minimalist.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minimilist.Enemies
{
    [RequireComponent(typeof(Collider2D))]
    public class EnemyDetection : MonoBehaviour
    {
        [field:SerializeField] public bool HasDetected {  get; private set; }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.TryGetComponent(out PlayerMovements player))
            {
                HasDetected = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if(other.TryGetComponent(out PlayerMovements player))
            {
                HasDetected = false;
            }
        }
    }
}