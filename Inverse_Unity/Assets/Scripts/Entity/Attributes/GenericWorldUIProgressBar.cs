using UnityEngine;
using UnityEngine.UI;

public class GenericWorldUIProgressBar : MonoBehaviour
{
    [SerializeField] private Gradient _progressGradient;
    [SerializeField] private Image _progressBarForegroundSprite;
    [SerializeField] private float _lerpSpeed = 2f;
    [SerializeField]
    private float _target = 1f;

    private void Awake()
    {
        if (_progressBarForegroundSprite == null)
        {
            _progressBarForegroundSprite = GetComponent<Image>();
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
        }

    }
}
