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

        public event Action OnInteract;

        // Private Fields
        private PlayerControls _inputs;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                DestroyImmediate(Instance.gameObject);

            _inputs = new PlayerControls();
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
                Debug.Log("Realm Switched!");
            };
        }

        private void OnDisable()
        {
            _inputs.Disable();
        }
        #endregion

        private void Update()
        {
            IsJumped = _inputs.Player.Jump.WasPerformedThisFrame();
        }
    }
}