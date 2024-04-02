using Managers.BWEffectManager;
using Minimalist.Level;
using Minimalist.Scene.Transition;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;

namespace Minimalist.Manager
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance { get; private set; }
        [field: SerializeField] public RealmManager RealmManager { get; private set; }

        private BWEffectManager effectManager;

        private void Awake()
        {
            if (Instance == null) // Singleton
            {
                Instance = this;
                //DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            if (RealmManager == null)
            {
                RealmManager = GetComponent<RealmManager>();
            }

            effectManager = GetComponent<BWEffectManager>();

            InitializeLevel();

            Cursor.visible = false;
        }

        public void InitializeLevel()
        {
            // Read from Scriptable Object

            RealmManager.InitializeRealmManager(LevelType.Light);
        }

        public void SwitchLevel(bool lightDark)
        {
            effectManager.SwapMode(() =>
            {
                RealmManager.OnLevelSwitch(lightDark ? LevelType.Dark : LevelType.Light);
            });
        }
    }
}
