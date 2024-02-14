using Minimalist.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minimalist.Player
{
    [RequireComponent(typeof(Collider2D))]
    public class PlayerInteractions : MonoBehaviour
    {
        private MyPlayerInput _inputs;
        [SerializeField] private IInteractable currentInteractable;
        [SerializeField] private Transform currentInteract;

        private void Awake()
        {
            _inputs = GetComponentInParent<MyPlayerInput>();
            var col = GetComponent<Collider2D>();
            col.isTrigger = true;
        }

        private void OnEnable()
        {
            _inputs.OnInteract += OnInteract;
        }

        private void OnDisable()
        {
            _inputs.OnInteract -= OnInteract;
        }

        private void OnInteract()
        {
            currentInteractable?.Interact();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent(out IInteractable interactable))
            {
                currentInteractable = interactable;
                currentInteract = collision.transform;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.TryGetComponent(out IInteractable interactable))
            {
                if (currentInteractable == interactable)
                    currentInteractable = null;
                currentInteract = null;
            }
        }
    }
}