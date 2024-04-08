using System;

using UnityEngine;
using UnityEngine.UI;

namespace Minimalist.UI.UIElements
{
    public class UIBackground : UIElement
    {
        [SerializeField] private Image _bgImage;

        internal override void Awake()
        {
            if (_bgImage == null)
            {
                _bgImage = GetComponent<Image>();
            }

            base.Awake();
        }

        public override void OnTransition(Color elementColor)
        {
            D("OnTransition: " + elementColor);

            if (_bgImage != null)
            {
                _bgImage.color = elementColor;
            }
        }

        private static void D(string message)
        {
            Debug.Log("<<UIBackground>> " + message);
        }
    }
}