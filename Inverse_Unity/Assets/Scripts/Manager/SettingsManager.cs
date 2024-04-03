using Minimalist.Audio;
using Minimalist.Audio.Sound;
using Minimalist.Inverse;
using Minimalist.SaveSystem;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace Minimalist.Manager
{
    public class SettingsManager : MonoBehaviour
    {
        [SerializeField] private Slider _masterVolumeSlider;
        [SerializeField] private Image _masterVolumeImage;
        [SerializeField] private List<Sprite> _masterVolumeSprite = new List<Sprite>();

        [SerializeField] private Slider _musicVolumeSlider;
        [SerializeField] private Image _musicVolumeImage;
        [SerializeField] private List<Sprite> _musicVolumeSprite = new List<Sprite>();

        [SerializeField] private Slider _sfxVolumeSlider;
        [SerializeField] private Image _sfxVolumeImage;
        [SerializeField] private List<Sprite> _sfxVolumeSprite = new List<Sprite>();

        #region Slider
        public void OnMasterValueChanged(float newValue)
        {
            D("OnMasterValueChanged: " + newValue);

            if (_masterVolumeImage != null && _masterVolumeSprite.Count > 0)
            {
                int indexToSet;

                if (newValue == 0)
                {
                    indexToSet = 0;
                }
                else
                {
                    indexToSet = Mathf.Clamp((int)(newValue * _masterVolumeSprite.Count), 1, _masterVolumeSprite.Count - 1);
                }

                _masterVolumeImage.sprite = _masterVolumeSprite[indexToSet];

                AudioManager.SetMusicVolume(newValue * _musicVolumeSlider.value);
            }
        }

        public void OnMusicValueChanged(float newValue)
        {
            D("OnMusicValueChanged: " + newValue);

            if (_musicVolumeImage != null && _musicVolumeSprite.Count > 0)
            {
                int indexToSet;

                if (newValue == 0)
                {
                    indexToSet = 0;
                }
                else
                {
                    indexToSet = Mathf.Clamp((int)(newValue * _musicVolumeSprite.Count), 1, _musicVolumeSprite.Count - 1);
                }

                _musicVolumeImage.sprite = _musicVolumeSprite[indexToSet];

                AudioManager.SetMusicVolume(newValue * _masterVolumeSlider.value);
            }
        }

        public void OnSFXValueChanged(float newValue)
        {
            D("OnSFXValueChanged: " + newValue);

            if (_sfxVolumeImage != null && _sfxVolumeSprite.Count > 0)
            {
                int indexToSet;

                if (newValue == 0)
                {
                    indexToSet = 0;
                }
                else
                {
                    indexToSet = Mathf.Clamp((int)(newValue * _sfxVolumeSprite.Count), 1, _sfxVolumeSprite.Count - 1);
                }

                _sfxVolumeImage.sprite = _sfxVolumeSprite[indexToSet];

                AudioManager.SetSFXVolume(newValue);
            }
        }

        private void InitValues()
        {
            if (_masterVolumeSlider != null)
            {
                _masterVolumeSlider.value = GameAttributes.Settings_MasterVolume;
            }

            if (_musicVolumeSlider != null)
            {
                _musicVolumeSlider.value = GameAttributes.Settings_MusicVolume;
            }

            if (_sfxVolumeSlider != null)
            {
                _sfxVolumeSlider.value = GameAttributes.Settings_SFXVolume;
            }
        }
        #endregion

        private void OnEnable()
        {
            InitValues();
            Invoke(nameof(EnableBackground), 0.15f);
        }

        private void OnDisable()
        {
            DisableBackground();
            //Invoke(nameof(DisableBackground), 0.25f);
        }

        private void EnableBackground()
        {
            if (MenuUIManager.Instance != null)
            {
                MenuUIManager.Instance.OnButtonHoverEnter("settings");
            }
        }

        private void DisableBackground()
        {
            if (MenuUIManager.Instance != null)
            {
                MenuUIManager.Instance.OnButtonHoverExit("settings");
            }
        }

        public void OnSavePressed()
        {
            GameAttributes.Settings_MasterVolume = _masterVolumeSlider.value;
            SaveManager.SaveData(Constants.SaveSystem.SETTINGS_MASTER_VOLUME, GameAttributes.Settings_MasterVolume);

            GameAttributes.Settings_MusicVolume = _musicVolumeSlider.value;
            SaveManager.SaveData(Constants.SaveSystem.SETTINGS_MUSIC_VOLUME, GameAttributes.Settings_MusicVolume);

            GameAttributes.Settings_SFXVolume = _sfxVolumeSlider.value;
            SaveManager.SaveData(Constants.SaveSystem.SETTINGS_SOUND_VOLUME, GameAttributes.Settings_SFXVolume);

            gameObject.SetActive(false);
        }

        public void OnSaveHover()
        {
            OnHover();
        }

        public void OnCancelPressed()
        {
            OnMasterValueChanged(GameAttributes.Settings_MasterVolume);
            OnMusicValueChanged(GameAttributes.Settings_MusicVolume);
            OnSFXValueChanged(GameAttributes.Settings_SFXVolume);

            gameObject.SetActive(false);
        }

        public void OnCancelHover()
        {
            OnHover();
        }

        private void OnHover()
        {
            var tempMasterVolume = GameAttributes.Settings_MasterVolume;
            var tempSFXVolume = GameAttributes.Settings_SFXVolume;

            if (_masterVolumeSlider != null)
            {
                GameAttributes.Settings_MasterVolume = _masterVolumeSlider.value;
            }

            if (_sfxVolumeSlider != null)
            {
                GameAttributes.Settings_SFXVolume = _sfxVolumeSlider.value;
            }

            AudioManager.PlaySFX(SoundType.UI_Hover);

            GameAttributes.Settings_MasterVolume = tempMasterVolume;
            GameAttributes.Settings_SFXVolume = tempSFXVolume;
        }

        private static void D(string message)
        {
            //Debug.Log("<<SettingsManager>> " + message);
        }
    }
}