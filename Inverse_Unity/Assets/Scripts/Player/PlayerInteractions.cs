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
        private IInteractable currentInteractable;
        [SerializeField] private Transform currentInteract; // To see which object is currently interactable

        private void Awake()
        {
            _inputs = GetComponentInParent<MyPlayerInput>();
            var col = GetComponent<Collider2D>();
            col.isTrigger = true; // just to be safe
        }

        private void OnEnable()
        {
            _inputs.OnInteract += OnInteract;
            _inputs.OnDie += OnDie;
        }

        private void OnDisable()
        {
            _inputs.OnInteract -= OnInteract;
            _inputs.OnDie -= OnDie;
        }

        private void OnInteract()
        {
            currentInteractable?.Interact();
        }

        private void OnDie() => gameObject.SetActive(false);

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