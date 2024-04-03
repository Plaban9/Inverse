using Minimalist.Audio.Sound;
using Minimalist.Audio;

using UnityEngine;
using Minimalist.Manager;

public class UICustomAction : MonoBehaviour, IUICustomAction
{
    private bool _isLevelBeingLoaded = false;

    public void OnInteractableClick(string elementName)
    {
        var customElementName = elementName.ToLower();

        D("OnInteractableClick: " + elementName);

        switch (customElementName)
        {
            case "quit":
                AudioManager.PlaySFX(SoundType.UI_Quit);
                Invoke(nameof(QuitGame), 1.5f);
                break;

            case "play":
                AudioManager.PlaySFX(SoundType.UI_Click);
                Play();
                break;

            case "settings":
                AudioManager.PlaySFX(SoundType.UI_Click);
                Settings();
                break;

            default:
                AudioManager.PlaySFX(SoundType.UI_Click);
                break;
        }
    }

    public void OnInteractableHover(string elementName)
    {
        var customElementName = elementName.ToLower();

        switch (customElementName)
        {
            default:
                AudioManager.PlaySFX(SoundType.UI_Hover);
                break;
        }
    }

    #region Actions
    private void QuitGame()
    {
        Application.Quit();
    }

    private void Play()
    {
        if (!_isLevelBeingLoaded)
        {
            _isLevelBeingLoaded = true;
            AudioManager.PlaySFX(SoundType.UI_Click);
            SceneManager.Instance.LoadScene("Story", "CrossFade");
        }
    }

    private void Settings()
    {
        if (MenuUIManager.Instance != null)
        {
            MenuUIManager.Instance.EnableSettings();
        }
    }
    #endregion

    private static void D(string message)
    {
        //Debug.Log("<<UICustomAction>> " + message);
    }
}
