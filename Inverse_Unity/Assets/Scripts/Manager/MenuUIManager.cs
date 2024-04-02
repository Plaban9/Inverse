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
        #endregion

        private static void D(string message)
        {
            //Debug.Log("<<MenuUIManager>> " + message);
        }
    }
}
