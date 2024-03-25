using Minimalist.Audio;
using Minimalist.Audio.Music;
using Minimalist.Audio.Sound;
using Minimalist.Inverse;
using Minimalist.Manager;
using Minimalist.SaveSystem;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Minimalist.Tests
{

    public class TestScript_Plaban : MonoBehaviour
    {
        [SerializeField] string sceneName;
        [SerializeField] string transitionName;

        bool isDark;

        void Start()
        {
            isDark = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.Instance.LoadScene(sceneName, transitionName);
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                isDark = !isDark;
                LevelManager.Instance.SwitchLevel(isDark);
            }

            if (Input.GetKeyDown(KeyCode.M))
            {
                AudioManager.PlayMusic(MusicType.Menu);
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                AudioManager.PlayMusic(MusicType.Gameplay);
            }
            
            if (Input.GetKeyDown(KeyCode.J))
            {
                AudioManager.PlaySFX(SoundType.Player_Jump);
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                SaveManager.SaveData(Constants.SaveSystem.GAMEPLAY_LAST_LEVEL_PLAYED, ++GameAttributes.Stat_LastLevelPlayed);
            }
        }
    }
}