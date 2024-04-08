using UnityEngine;
using UnityEngine.UI;

namespace Minimalist.Entity.Attributes
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Gradient _healthGradient;
        [SerializeField] private Image _healthBarForegroundSprite;
        [SerializeField] private float _lerpSpeed = 2f;
        [SerializeField]
        private float _target = 1f;

        private void Awake()
        {
            if (_healthBarForegroundSprite == null)
            {
                _healthBarForegroundSprite = GetComponent<Image>();
            }
        }

        private void Update()
        {
            UpdateHealthBar();
        }

        public void SetHealth(float maxHealth, float currentHealth)
        {
            var percentage = currentHealth / maxHealth;

            if (_healthBarForegroundSprite != null)
            {
                _target = percentage;
            }
        }

        private void UpdateHealthBar()
        {
            if (_healthBarForegroundSprite != null)
            {
                var lerpedNormalizedValue = Mathf.MoveTowards(_healthBarForegroundSprite.fillAmount, _target, _lerpSpeed * Time.deltaTime);
                var healthBarColor = _healthGradient.Evaluate(lerpedNormalizedValue);

                _healthBarForegroundSprite.color = healthBarColor;
                _healthBarForegroundSprite.fillAmount = lerpedNormalizedValue;
            }

        }
    }
}