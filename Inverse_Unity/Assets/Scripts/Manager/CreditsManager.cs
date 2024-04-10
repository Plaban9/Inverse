using Minimalist.Audio;
using UnityEngine;

namespace Minimalist.Manager
{
    public class CreditsManager : MonoBehaviour
    {
        [SerializeField] private float _showDurationInSecs = 5f;
        [SerializeField] private float _timePressed;
        [SerializeField] private GameObject _backButton;
        [SerializeField] private Animator _creditsAnimator;

        [SerializeField] private float _defaultScrollSpeed = 1f;
        [SerializeField] private float _fasterScrollSpeed = 0.5f;
        [SerializeField] private float _slowerScrollSpeed = 0.005f;

        private void Start()
        {
            if (_creditsAnimator == null)
            {
                _creditsAnimator = GetComponent<Animator>();
            }
        }

        private void OnEnable()
        {
            Invoke(nameof(StartCredits), 9f);
        }

        private void Update()
        {
            if (_timePressed + _showDurationInSecs < Time.time)
            {
                DisableBackButton();
            }

            HandleAnimatorSpeed();

            HandleBackButton(false);
        }

        private void StartCredits()
        {
            AudioManager.PlayMusic(Audio.Music.MusicType.Credits);

            if (_creditsAnimator != null)
            {
                _creditsAnimator.SetTrigger("StartCredit");
            }
        }

        public void HandleBackButton(bool enableButton)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || enableButton)
            {
                EnableBackButton();
            }
        }

        public void DisableBackButton()
        {
            if (_backButton != null)
            {
                _backButton.SetActive(false);
            }
        }

        public void EnableBackButton()
        {
            if (_backButton != null)
            {
                _timePressed = Time.time;

                _backButton.SetActive(true);
            }
        }

        public void OnBackButtonHover()
        {
            AudioManager.PlaySFX(Audio.Sound.SoundType.UI_Hover);
        }

        public void BackToMenu()
        {
            AudioManager.PlaySFX(Audio.Sound.SoundType.UI_Click);
            SceneManager.Instance.LoadScene("Menu", "CrossFade");
        }

        public void HandleAnimatorSpeed()
        {
            if (_creditsAnimator != null)
            {
                var animatorInfo = _creditsAnimator.GetCurrentAnimatorClipInfo(0);

                if (!animatorInfo[0].clip.name.Equals("Credits_Scroll"))
                {
                    return;
                }

                if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.DownArrow))
                {
                    _creditsAnimator.speed = _fasterScrollSpeed;
                }
                else if (Input.GetMouseButton(1) || Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.UpArrow))
                {
                    _creditsAnimator.speed = _slowerScrollSpeed;
                }
                else
                {
                    _creditsAnimator.speed = _defaultScrollSpeed;
                }
            }
        }
    }
}
