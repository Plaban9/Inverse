using Minimalist.Audio;
using Minimalist.Audio.Music;
using Minimalist.Audio.Sound;

using System.Collections.Generic;

using TMPro;

using UnityEngine;

namespace Minimalist.Manager
{
    public class MenuUIManager : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private TextMeshProUGUI _playButton;
        [SerializeField] private TextMeshProUGUI _quitButton;
        [SerializeField] private Dictionary<string, TextMeshProUGUI> _menuButtonList;

        private void Awake()
        {
            if (_menuButtonList == null)
            {
                _menuButtonList = new Dictionary<string, TextMeshProUGUI>();
            }

            RefreshDictionary();
        }

        private void RefreshDictionary()
        {
            _menuButtonList.Clear();
            _menuButtonList["play"] = _playButton;
            _menuButtonList["quit"] = _quitButton;
        }

        private void Start()
        {
            AudioManager.PlayMusic(MusicType.Menu, 1f, true);
        }

        public void OnButtonHoverEnter(string elementName)
        {
            d("OnButtonHoverEnter: " + elementName);

            AudioManager.PlaySFX(SoundType.UI_Hover);

            _camera.backgroundColor = Color.white;
            _title.color = Color.black;


            foreach (var fontItem in _menuButtonList)
            {
                if (fontItem.Key.Equals(elementName))
                {
                    fontItem.Value.color = Color.black;
                }
            }
        }

        public void OnButtonHoverExit(string elementName)
        {
            d("OnButtonHoverExit: " + elementName);

            _camera.backgroundColor = Color.black;
            _title.color = Color.white;

            foreach (var fontItem in _menuButtonList)
            {
                fontItem.Value.color = Color.white;
            }
        }

        public void OnButtonPressed(string action)
        {
            d("OnButtonPressed: " + action);

            switch (action)
            {
                case "play":
                    AudioManager.PlaySFX(SoundType.UI_Click);
                    SceneManager.Instance.LoadScene("Level1", "CrossFade");
                    break;
                case "quit":
                    AudioManager.PlaySFX(SoundType.UI_Quit);
                    Invoke("QuitGame", 1f);
                    break;
            }
        }

        private void QuitGame()
        {
            Application.Quit();
        }

        private static void d(string message)
        {
            //Debug.Log("<<MenuUIManager>> " + message);
        }
    }
}
