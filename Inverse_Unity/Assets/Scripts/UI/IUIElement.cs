using UnityEngine;

namespace Minimalist.UI
{
    public interface IUIElement
    {
        public void OnHoverEnter(Color elementColor);
        public void OnHoverExit(Color elementColor);
        public void OnClick(Color elementColor);
        public void OnTransition(Color elementColor);
    }
}