using TMPro;

using UnityEngine;

namespace Minimalist.UI.UIElements
{
    [RequireComponent(typeof(UICustomAction))]
    public class UIInteractable : UIElement
    {
        [SerializeField] private TextMeshProUGUI _textElement;
        [SerializeField] private IUICustomAction _customAction;

        internal override void Awake()
        {
            if (_textElement == null)
            {
                _textElement = GetComponentInChildren<TextMeshProUGUI>();
            }

            if (_customAction == null)
            {
                _customAction = GetComponentInChildren<UICustomAction>();
            }

            base.Awake();
        }

        public override void OnClick(Color elementColor)
        {
            _customAction.OnInteractableClick(name);

            OnTransition(elementColor);
        }

        public override void OnHoverEnter(Color elementColor)
        {
            D("OnHoverEnter: " + elementColor);

            _customAction.OnInteractableHover(name);

            OnTransition(elementColor);
        }

        public override void OnHoverExit(Color elementColor)
        {
            OnTransition(elementColor);
        }

        public override void OnTransition(Color elementColor)
        {
            _textElement.color = elementColor;
        }

        private static void D(string message)
        {
            //Debug.Log("<<UIInteractable>> " + message);
        }
    }
}