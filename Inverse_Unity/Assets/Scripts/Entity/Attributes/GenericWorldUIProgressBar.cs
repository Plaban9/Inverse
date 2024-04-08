using DG.Tweening;

using System.Collections;

using UnityEngine;
using UnityEngine.UI;

public class GenericWorldUIProgressBar : MonoBehaviour
{
    [SerializeField] private GameObject _progressBarObject;
    [SerializeField] private Gradient _progressGradient;
    [SerializeField] private Image _progressBarForegroundSprite;
    [SerializeField] private float _lerpSpeed = 2f;
    [SerializeField] private float _target = 1f;
    [SerializeField] private bool _hideProgressBarAtMax = false;

    private void Awake()
    {
        if (_progressBarForegroundSprite == null)
        {
            _progressBarForegroundSprite = GetComponent<Image>();

            HandleProgressBarVisibility();
        }
    }

    private void Update()
    {
        UpdateProgress();
    }

    public void SetProgress(float maxProgress, float currentProgress)
    {
        var percentage = currentProgress / maxProgress;

        if (_progressBarForegroundSprite != null)
        {
            _target = percentage;
        }
    }

    private void UpdateProgress()
    {
        if (_progressBarForegroundSprite != null)
        {
            var lerpedNormalizedValue = Mathf.MoveTowards(_progressBarForegroundSprite.fillAmount, _target, _lerpSpeed * Time.deltaTime);
            var progressBarColor = _progressGradient.Evaluate(lerpedNormalizedValue);

            _progressBarForegroundSprite.color = progressBarColor;
            _progressBarForegroundSprite.fillAmount = lerpedNormalizedValue;
            HandleProgressBarVisibility();
        }
    }

    private void HandleProgressBarVisibility()
    {
        if (_hideProgressBarAtMax && _progressBarForegroundSprite.fillAmount == 1f)
        {
            _progressBarObject.transform.DOScale(Vector3.zero, 1f);
            Invoke(nameof(DisableProgressBar), 1.5f);
        }
        else
        {
            EnableProgressBar();
        }
    }

    private void DisableProgressBar()
    {
        if (_progressBarObject != null && (_hideProgressBarAtMax && _progressBarForegroundSprite.fillAmount == 1f))
        {
            _progressBarObject.SetActive(false);
        }
    }

    private void EnableProgressBar()
    {
        if (_progressBarObject != null && !(_hideProgressBarAtMax && _progressBarForegroundSprite.fillAmount == 1f))
        {
            _progressBarObject.transform.DOScale(Vector3.one, 0.5f);
            _progressBarObject.SetActive(true);
        }
    }
}
