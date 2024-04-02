using UnityEngine;

namespace Minimalist.UI
{
    [RequireComponent(typeof(UIProperty))]
    public abstract class UIElement : MonoBehaviour, IUIElement
    {
        [SerializeField] private UIProperty _uiProperty;

        public UIProperty UiProperty { get => _uiProperty; private set => _uiProperty = value; }

        internal virtual void Awake()
        {
            if (UiProperty == null)
            {
                UiProperty = GetComponent<UIProperty>();
            }

            OnTransition(UiProperty.DefaultColor);
        }

        public virtual void OnClick(Color elementColor)
        {
            OnTransition(elementColor);
        }

        public virtual void OnHoverEnter(Color elementColor)
        {
            OnTransition(elementColor);
        }

        public virtual void OnHoverExit(Color elementColor)
        {
            OnTransition(elementColor);
        }

        public virtual void OnTransition(Color elementColor)
        {

        }

        public bool Equals(string name)
        {
            if (name == null)
            {
                return false;
            }

            return (this.name.Equals(name));
        }
    }
}