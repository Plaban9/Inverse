using TMPro;

using UnityEngine;

namespace Minimalist.UI.UIElements
{
    public class UINonInteractable : UIElement
    {
        [SerializeField] private TextMeshProUGUI _textElement;

        internal override void Awake()
        {
            if (_textElement == null)
            {
                _textElement = GetComponent<TextMeshProUGUI>();
            }

            base.Awake();
        }

        public override void OnTransition(Color elementColor)
        {
            D("OnTransition: " + elementColor);

            _textElement.color = elementColor;
        }

        private static void D(string message)
        {
            //Debug.Log("<<UINonInteractable>> " + message);
        }
    }
}