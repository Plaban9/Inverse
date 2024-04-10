using Minimalist.Audio;
using Minimalist.Audio.Music;
using Minimalist.UI;

using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Minimalist.Manager
{
    public class MenuUIManager : MonoBehaviour
    {
        [SerializeField] private List<UIElement> _uiElements = new List<UIElement>();

        [SerializeField] private SettingsManager _settingsManager;

        [SerializeField] private Animator _playHoverAnimaton;
        [SerializeField] private Animator _creditsHoverAnimaton;
        [SerializeField] private Animator _settingsHoverAnimaton;
        [SerializeField] private Animator _quitHoverAnimaton;
        public static MenuUIManager Instance { get; private set; }

        private void Awake()
        {
            if (_uiElements.Count == 0)
            {
                _uiElements = FindObjectsByType<UIElement>(FindObjectsSortMode.None).ToList();
            }

            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            Cursor.visible = true;
        }

        private void Start()
        {
            AudioManager.PlayMusic(MusicType.Menu, 1f, true);

            if (_settingsManager == null)
            {
                _settingsManager = GetComponentInChildren<SettingsManager>(true);
            }
        }

        #region UI Element Actions
        public void OnButtonHoverEnter(string elementName)
        {
            D("OnButtonHoverEnter: " + elementName);

            var uiElement = _uiElements.Find(element => element.UiProperty.Name.Trim().ToLower().Equals(elementName.ToLower()));

            if (uiElement == null)
            {
                D("OnButtonHoverEnter - Element not found with name: " + elementName);
                return;
            }

            var uiProperty = uiElement.UiProperty;

            var complementColor = uiProperty.ComplementColor;
            var transitionColor = uiProperty.HoverColor;

            foreach (var elem in _uiElements)
            {
                if (elem.UiProperty.Name.ToLower().Equals(elementName.ToLower()) || elem.UiProperty.KeepColorAsSelected)
                {
                    elem.OnHoverEnter(transitionColor);
                    continue;
                }

                elem.OnTransition(complementColor);
            }

        }

        public void OnButtonHoverExit(string elementName)
        {
            D("OnButtonHoverExit: " + elementName);

            var uiElement = _uiElements.Find(element => element.UiProperty.Name.Trim().ToLower().Equals(elementName.ToLower()));

            if (uiElement == null)
            {
                D("OnButtonHoverExit - Element not found with name: " + elementName);
                return;
            }

            foreach (var elem in _uiElements)
            {
                elem.OnHoverExit(elem.UiProperty.DefaultColor);
            }
        }

        public void OnButtonPressed(string elementName)
        {
            D("OnButtonPressed: " + elementName);

            var uiElement = _uiElements.Find(element => element.UiProperty.Name.Trim().ToLower().Equals(elementName.ToLower()));

            if (uiElement == null)
            {
                D("OnButtonPressed - Element not found with name: " + elementName);
                return;
            }

            uiElement.OnClick(uiElement.UiProperty.ClickColor);
        }
        #endregion

        #region Helpers
        private Color LerpFunction(Color startColor, Color endColor, float duration_ms)
        {
            // TODO: Lerp Here
            return startColor;
        }

        internal void EnableSettings()
        {
            if (_settingsManager != null)
            {
                _settingsManager.gameObject.SetActive(true);
            }
        }
        #endregion

        #region Hover Animation
        public void OnPlayHoverEnter()
        {
            if (_playHoverAnimaton != null)
            {
                _playHoverAnimaton.SetBool("ease_in", true);
            }
        }

        public void OnPlayHoverExit()
        {
            if (_playHoverAnimaton != null)
            {
                _playHoverAnimaton.SetBool("ease_in", false);
            }
        }

        public void OnCreditsHoverEnter()
        {
            if (_creditsHoverAnimaton != null)
            {
                _creditsHoverAnimaton.SetBool("ease_in", true);
            }
        }

        public void OnCreditsHoverExit()
        {
            if (_creditsHoverAnimaton != null)
            {
                _creditsHoverAnimaton.SetBool("ease_in", false);
            }
        }

        public void OnSettingsHoverEnter()
        {
            if (_settingsHoverAnimaton != null)
            {
                _settingsHoverAnimaton.SetBool("ease_in", true);
            }
        }

        public void OnSettingsHoverExit()
        {
            if (_settingsHoverAnimaton != null)
            {
                _settingsHoverAnimaton.SetBool("ease_in", false);
            }
        }

        public void OnQuitHoverEnter()
        {
            if (_quitHoverAnimaton != null)
            {
                _quitHoverAnimaton.SetBool("ease_in", true);
            }
        }

        public void OnQuitHoverExit()
        {
            if (_quitHoverAnimaton != null)
            {
                _quitHoverAnimaton.SetBool("ease_in", false);
            }
        }
        #endregion

        private static void D(string message)
        {
            //Debug.Log("<<MenuUIManager>> " + message);
        }
    }
}
