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
    [SerializeField] private float _stayDuration = 1f;
    [SerializeField] private bool _hideProgressBarAtMax = false;

    private bool _shouldUpdateProgress = false;

    private void Awake()
    {
        _shouldUpdateProgress = false;

        if (_progressBarForegroundSprite == null)
        {
            _progressBarForegroundSprite = GetComponent<Image>();
        }

        SetProgress(10f, 10f);
    }

    private void Update()
    {
        if (_shouldUpdateProgress)
        {
            UpdateProgress();
        }
    }

    public void SetProgress(float maxProgress, float currentProgress)
    {
        var percentage = currentProgress / maxProgress;

        if (_progressBarForegroundSprite != null)
        {
            _target = percentage;
        }

        if (_hideProgressBarAtMax)
        {
            ScaleProgressBarAndUpdate(Vector3.one, 1f);
        }
        else
        {
            SwitchUpdateProcess(true);
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

            if (_progressBarForegroundSprite.fillAmount >= 1f && _hideProgressBarAtMax)
            {
                SwitchUpdateProcess(false);
                Invoke(nameof(CloseProgressBar), _stayDuration);
            }
        }
    }

    private void CloseProgressBar()
    {
        D("Close Called");
        ScaleProgressBar(Vector3.zero, 1f);
    }

    private void ScaleProgressBarAndUpdate(Vector3 endScale, float transitionTimeInSecs)
    {
        D("ScaleProgressBarAndUpdate Called");
        _progressBarObject.transform.DOScale(endScale, transitionTimeInSecs).OnComplete(() => SwitchUpdateProcess(true));
    }

    private void ScaleProgressBar(Vector3 endScale, float transitionTimeInSecs)
    {
        D("ScaleProgressBar Called");
        _progressBarObject.transform.DOScale(endScale, transitionTimeInSecs);
    }

    private void SwitchUpdateProcess(bool shouldUpdate)
    {
        _shouldUpdateProgress = shouldUpdate;
    }

    private static void D(string message)
    {
        Debug.Log("<<GenericWorldUIProgressBar>>" + message);
    }
}
