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

        private readonly List<IInteractable> interactablesList = new();

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
                if(!interactablesList.Contains(interactable))
                {
                    interactablesList.Add(interactable);
                }
                string msg = "List now contains:\n";
                interactablesList.ForEach(x => msg += x.ToString() + "\n");
                Debug.Log(msg);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.TryGetComponent(out IInteractable interactable) && interactablesList.Contains(interactable))
            {
                interactablesList.Remove(interactable);
                if (currentInteractable == interactable)
                    currentInteractable = null;
                string msg = "List now contains:\n";
                interactablesList.ForEach(x => msg += x.ToString() + "\n");
                Debug.Log(msg);
            }
        }
    }
}