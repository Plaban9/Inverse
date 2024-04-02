using System;

using UnityEngine;

namespace Minimalist.UI
{
    [Serializable]
    public class UIProperty : MonoBehaviour
    {
        [SerializeField] private string _name;
        [SerializeField] private bool _keepColorAsSelected;
        [SerializeField] private Color _defaultColor = Color.white;
        [SerializeField] private Color _hoverColor = Color.black;
        [SerializeField] private Color _clickColor = Color.black;
        [SerializeField] private Color _complementColor = Color.white;

        #region Properties
        public string Name { get => _name; set => _name = value; }
        public Color DefaultColor { get => _defaultColor; private set => _defaultColor = value; }
        public Color HoverColor { get => _hoverColor; private set => _hoverColor = value; }
        public Color ClickColor { get => _clickColor; private set => _clickColor = value; }
        public bool KeepColorAsSelected { get => _keepColorAsSelected; private set => _keepColorAsSelected = value; }
        public Color ComplementColor { get => _complementColor; private set => _complementColor = value; }
        #endregion

    }
}