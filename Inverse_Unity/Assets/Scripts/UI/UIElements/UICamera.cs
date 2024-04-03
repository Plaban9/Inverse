using System;

using UnityEngine;

namespace Minimalist.UI.UIElements
{
    public class UICamera : UIElement
    {
        [SerializeField] private Camera _camera;

        internal override void Awake()
        {
            if (_camera == null)
            {
                _camera = Camera.main;
            }

            base.Awake();
        }

        public override void OnTransition(Color elementColor)
        {
            D("OnTransition: " + elementColor);

            if (_camera != null)
            {
                _camera.backgroundColor = elementColor;
            }
        }

        private static void D(string message)
        {
            //Debug.Log("<<UICamera>> " + message);
        }
    }
}