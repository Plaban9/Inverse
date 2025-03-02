using Minimalist.Audio.Sound;
using Minimalist.Audio;
using Minimalist.Manager;
using Minimilist.Player.PlayerActions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Minimalist.Player
{
    public class MyPlayerInput : MonoBehaviour
    {
        public static MyPlayerInput Instance { get; set; }

        // Public Fields
        public float MoveVector { get; private set; }
        public bool IsJumped { get; private set; }
        public bool IsDashed {  get; private set; }

        public event Action OnInteract;
        public event Action OnDie;
        public event Action OnRealmSwitch;

        // Private Fields
        private PlayerControls _inputs;
        private PlayerInput playerInput;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                DestroyImmediate(Instance.gameObject);

            _inputs = new PlayerControls();
            playerInput = GetComponent<PlayerInput>();
        }

        #region Input Events

        private void OnEnable()
        {
            _inputs.Enable();
            _inputs.Player.Move.performed += ctx => MoveVector = ctx.ReadValue<float>();
            _inputs.Player.Move.canceled += ctx => MoveVector = 0f;
            _inputs.Player.Interact.performed += ctx => OnInteract?.Invoke();
            _inputs.Player.RealmSwitch.performed += ctx =>
            {
                var isDarkRealm = LevelManager.Instance.RealmManager.GetCurrentLevelType() == Level.LevelType.Dark;
                LevelManager.Instance.SwitchLevel(!isDarkRealm);
                OnRealmSwitch?.Invoke();
                Debug.Log("Realm Switched!");
            };
        }

        private void OnDisable()
        {
            _inputs.Disable();
            _inputs.Dispose();
        }
        #endregion

        private void Update()
        {
            IsJumped = _inputs.Player.Jump.WasPerformedThisFrame();
            IsDashed = _inputs.Player.Dash.WasPerformedThisFrame();
        }

        public void Die()
        {
            _inputs.Player.Disable();
            OnDie?.Invoke();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.CompareTag("Death"))
            {
                Debug.Log("Die: " + transform.name);
                Die();
            }
        }

        public string GetCurrentControlScheme()
        {
            return playerInput.currentControlScheme;
        }
    }
}