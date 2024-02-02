namespace Managers.BWEffectManager
{
    using UnityEngine;
    using Managers.BWState;
    using System.Collections;

    public class BWEffectManager : MonoBehaviour
    {

        [Header("Data")]
        [SerializeField] private Material       _frontMaterial;
        [SerializeField] private Material       _backMaterial;


        [Header("Specifications")]
        [SerializeField] private float          _swapDurationInMs;
        [SerializeField] private AnimationCurve _transition;


        [Header("Debug")]

        [Range(0.0f, 1.0f)][SerializeField] private float          _percent;


        public BWState GetMode()
        {
            if (_percent == 1.0f)
            {
                return BWState.DARK_MODE;
            }else if(_percent == 0.0f)
            {
                return BWState.LIGHT_MODE;
            }
            return BWState.TRANSITION;

        }

        public float GetPercentage()
        {
            return _percent;
        }

        public void SetPercent(float percent)
        {
            _percent = Mathf.Clamp(percent, 0.0f, 1.0f);
            _frontMaterial.SetFloat("_Interpolate", _percent);
            _backMaterial.SetFloat("_Interpolate", _percent);
        }

        public void SwapMode()
        {
            Managers.BWState.BWState currentState = GetMode();


            if (currentState == BWState.TRANSITION)
            {
                return;
            }

            StartCoroutine(SwapCoroutine());
        }

        private IEnumerator SwapCoroutine()
        {

            float currentPct = GetPercentage();
            float finalPct = currentPct == 1.0f ? 0.0f : 1.0f;

            for (float i = 0.0f; i < _swapDurationInMs; i++)
            {
                float t = i / _swapDurationInMs;
                float transAmount = _transition.Evaluate(t);
                SetPercent(
                    currentPct * (1.0f - transAmount) +
                    finalPct * (transAmount)
                    );
                yield return null;
            }
            SetPercent(finalPct);
        }
    }

}
