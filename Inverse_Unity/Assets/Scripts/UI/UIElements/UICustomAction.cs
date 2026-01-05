using Minimalist.Audio;
using Minimalist.Audio.Sound;
using Minimalist.Inverse;
using Minimalist.Manager;
using Minimalist.SaveSystem;

using UnityEngine;

public class UICustomAction : MonoBehaviour, IUICustomAction
{
    private static bool _isSceneBeingLoaded = false;

    public void Awake()
    {
        _isSceneBeingLoaded = false;
    }
    public void OnInteractableClick(string elementName)
    {
        var customElementName = elementName.ToLower();

        D("OnInteractableClick: " + elementName);

        switch (customElementName)
        {
            case "quit":
                AudioManager.PlaySFX(SoundType.UI_Click);
                Invoke(nameof(QuitGame), 1.5f);
                break;

            case "play":
                Play();
                break;

            case "credits":
                Credits();
                break;

            case "settings":
                Settings();
                break;

            case "gameover_restart":
            case "pausepanel_restart":
                AudioManager.PlaySFX(SoundType.UI_Click);
                RestartLevel();
                break;

            case "gameover_menu":
            case "pausepanel_menu":
                AudioManager.PlaySFX(SoundType.UI_Click);
                LoadMenuScene();
                break;

            default:
                AudioManager.PlaySFX(SoundType.UI_Click);
                break;
        }
    }

    public void OnInteractableHoverEnter(string elementName)
    {
        var customElementName = elementName.ToLower();

        switch (customElementName)
        {
            case "play":
                PlayHoverEnter();
                break;

            case "credits":
                CreditsHoverEnter();
                break;

            case "settings":
                SettingsHoverEnter();
                break;

            case "quit":
                QuitHoverEnter();
                break;

            default:
                AudioManager.PlaySFX(SoundType.UI_Hover);
                break;
        }
    }

    public void OnInteractableHoverExit(string elementName)
    {
        var customElementName = elementName.ToLower();

        switch (customElementName)
        {
            case "play":
                PlayHoverExit();
                break;

            case "credits":
                CreditsHoverExit();
                break;

            case "settings":
                SettingsHoverExit();
                break;

            case "quit":
                QuitHoverExit();
                break;

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

    private void QuitHoverEnter()
    {
        AudioManager.PlaySFX(SoundType.UI_Hover);

        if (MenuUIManager.Instance != null)
        {
            MenuUIManager.Instance.OnQuitHoverEnter();
        }
    }

    private void QuitHoverExit()
    {
        if (MenuUIManager.Instance != null)
        {
            MenuUIManager.Instance.OnQuitHoverExit();
        }
    }

    private void Play()
    {
        //if (!_isSceneBeingLoaded)
        {
            _isSceneBeingLoaded = true;
            AudioManager.PlaySFX(SoundType.UI_Click);
            //SceneManager.Instance.LoadScene("Story", "CrossFade"); // Temporarily disabled story scene for demo purposes

            bool isTutorialCompleted = SaveManager.ReadData(Constants.SaveSystem.STAT_TUTORIAL_COMPLETED, false);
            var sceneToLoad = isTutorialCompleted ? "Level1" : "Level_Tut";

            SceneManager.Instance.LoadScene(sceneToLoad, "CrossFade");
        }
    }

    private void PlayHoverEnter()
    {
        AudioManager.PlaySFX(SoundType.UI_Hover);

        if (MenuUIManager.Instance != null)
        {
            MenuUIManager.Instance.OnPlayHoverEnter();
        }
    }

    private void PlayHoverExit()
    {
        if (MenuUIManager.Instance != null)
        {
            MenuUIManager.Instance.OnPlayHoverExit();
        }
    }

    private void Credits()
    {
        if (MenuUIManager.Instance != null)
        {
            _isSceneBeingLoaded = true;
            AudioManager.PlaySFX(SoundType.UI_Click);
            SceneManager.Instance.LoadScene("Credits", "Clapper", false);
        }
    }

    private void CreditsHoverEnter()
    {
        AudioManager.PlaySFX(SoundType.UI_Hover);

        if (MenuUIManager.Instance != null)
        {
            MenuUIManager.Instance.OnCreditsHoverEnter();
        }
    }

    private void CreditsHoverExit()
    {
        if (MenuUIManager.Instance != null)
        {
            MenuUIManager.Instance.OnCreditsHoverExit();
        }
    }

    private void Settings()
    {
        if (MenuUIManager.Instance != null)
        {
            MenuUIManager.Instance.EnableSettings();
        }
    }

    private void SettingsHoverEnter()
    {
        AudioManager.PlaySFX(SoundType.UI_Hover);

        if (MenuUIManager.Instance != null)
        {
            MenuUIManager.Instance.OnSettingsHoverEnter();
        }
    }

    private void SettingsHoverExit()
    {
        if (MenuUIManager.Instance != null)
        {
            MenuUIManager.Instance.OnSettingsHoverExit();
        }
    }

    private void RestartLevel()
    {
        //if(!_isSceneBeingLoaded)
        {
            Debug.Log("Restarting level");
            _isSceneBeingLoaded = true;
            Time.timeScale = 1;
            SceneManager.Instance.LoadScene(SceneManager.Instance.ActiveScene, "CrossFade", false);
        }
    }

    private void LoadMenuScene()
    {
        //if (!_isSceneBeingLoaded)
        {
            _isSceneBeingLoaded = true;
            Time.timeScale = 1;
            SceneManager.Instance.LoadScene("Menu", "CrossFade");
        }
    }
    #endregion

    private static void D(string message)
    {
       //Debug.Log("<<UICustomAction>> " + message);
    }
}
