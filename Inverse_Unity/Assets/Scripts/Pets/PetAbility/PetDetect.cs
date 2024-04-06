using Minimalist.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minimalist.Pet.Ability
{
    [RequireComponent(typeof(Collider2D))]
    public class PetDetect : MonoBehaviour
    {
        [SerializeField] private List<Enemy> enemiesList;

        public bool HasDetected => enemiesList.Count > 0;

        private void Awake()
        {
            var col = GetComponent<Collider2D>();
            col.isTrigger = true;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if(other.TryGetComponent(out Enemy enemy) && !enemiesList.Contains(enemy))
            {
                enemiesList.Add(enemy);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if(other.TryGetComponent(out Enemy enemy) && enemiesList.Contains(enemy))
            {
                enemiesList.Remove(enemy);
            }
        }
    }
}