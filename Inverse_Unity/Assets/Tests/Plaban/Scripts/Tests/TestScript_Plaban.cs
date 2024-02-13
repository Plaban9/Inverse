using Minimalist.Manager;

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
        }
    }
}