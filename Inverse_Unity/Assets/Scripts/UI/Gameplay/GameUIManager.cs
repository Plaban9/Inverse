using Minimalist.Player;
using Minimalist.UI;
using Minimilist.Player.PlayerActions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject settingsUI;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private List<UIElement> _uiElements;
    private PlayerControls inputActions;
    private MyPlayerInput playerInput;

    private void Awake()
    {
        if (_uiElements.Count == 0)
        {
            _uiElements = FindObjectsByType<UIElement>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList();
        }
        inputActions = new PlayerControls();
        playerInput = FindAnyObjectByType<MyPlayerInput>();
    }

    private void OnEnable()
    {
        inputActions.UI.Enable();
        inputActions.UI.Pause.performed += OnPausePerformed;
        playerInput.OnDie += OnDie;
    }

    private void OnDie()
    {
        inputActions.UI.Disable();
        gameOverUI.SetActive(true);
    }

    private void OnPausePerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        HandlePause();
    }

    private void OnDisable()
    {
        inputActions.UI.Disable();
        inputActions.UI.Pause.performed -= OnPausePerformed;
        playerInput.OnDie -= OnDie;
    }

    private void Start()
    {
        pauseUI.SetActive(false);
        gameOverUI.SetActive(false);
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

    public void HandlePause()
    {
        _uiElements.ForEach(x => x.OnHoverExit(x.UiProperty.DefaultColor));

        var newPauseState = !pauseUI.activeInHierarchy;
        pauseUI.SetActive(newPauseState);
        settingsUI.SetActive(false);
        Time.timeScale = newPauseState ? 0 : 1;
    }
    #endregion

    private static void D(string message)
    {
        //Debug.Log("<<GameUIManager>> " + message);
    }
}
