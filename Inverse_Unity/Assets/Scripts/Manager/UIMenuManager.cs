using Minimalist.Audio.Music;
using Minimalist.Audio;
using Minimalist.UI;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using Minimalist.Audio.Sound;

namespace Minimalist.Manager
{
    public class UIMenuManager : MonoBehaviour
    {
        [SerializeField] private List<UIElement> _uiElements = new List<UIElement>();

        private void Awake()
        {
            if (_uiElements.Count == 0)
            {
                _uiElements = FindObjectsByType<UIElement>(FindObjectsSortMode.None).ToList();
            }
        }

        private void Start()
        {
            AudioManager.PlayMusic(MusicType.Menu, 1f, true);
        }

        #region UI Element Actions
        public void OnButtonHoverEnter(string elementName)
        {
            d("OnButtonHoverEnter: " + elementName);

            var uiElement = _uiElements.Find(element => element.name.ToLower().Equals(elementName));

            if (uiElement == null)
            {
                d("OnButtonHoverEnter - Element not found with name: " + elementName);
                return;
            }

            var uiProperty = uiElement.UiProperty;

            var complementColor = uiProperty.ComplementColor;
            var transitionColor = uiProperty.HoverColor;

            foreach (var elem in _uiElements)
            {
                if (elem.name.ToLower().Equals("camera"))
                {
                    elem.OnTransition(elem.UiProperty.ComplementColor);
                    continue;
                }

                if (elem.name.ToLower().Equals(elementName) || elem.UiProperty.KeepColorAsSelected)
                {
                    elem.OnHoverEnter(transitionColor);
                    continue;
                }

                elem.OnHoverEnter(complementColor);
            }

        }

        public void OnButtonHoverExit(string elementName)
        {
            d("OnButtonHoverExit: " + elementName);

            var uiElement = _uiElements.Find(element => element.name.ToLower().Equals(elementName));

            if (uiElement == null)
            {
                d("OnButtonHoverExit - Element not found with name: " + elementName);
                return;
            }

            foreach (var elem in _uiElements)
            {
                elem.OnHoverExit(elem.UiProperty.DefaultColor);
            }
        }

        public void OnButtonPressed(string elementName)
        {
            d("OnButtonPressed: " + elementName);

            var uiElement = _uiElements.Find(element => element.name.ToLower().Equals(elementName));

            if (uiElement == null)
            {
                d("OnButtonPressed - Element not found with name: " + elementName);
                return;
            }

            uiElement.OnClick(uiElement.UiProperty.ClickColor);
        }
        #endregion

        private static void d(string message)
        {
            Debug.Log("<<MenuUIManager>> " + message);
        }
    }
}