namespace Managers.BWEffectManager
{
    using UnityEngine;
    using Managers.BWState;
    public class BWEffectManager : MonoBehaviour
    {

        [Header("Data")]
        [SerializeField] private Material _bwMaterial;

        [Header("Control")]
        [SerializeField] private float _percent;

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
            _bwMaterial.SetFloat("_LightPct", _percent);
        }
    }

}
