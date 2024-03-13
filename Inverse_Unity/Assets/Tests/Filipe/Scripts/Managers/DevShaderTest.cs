using Managers.BWEffectManager;
using Managers.BWState;
using Minimalist.Effect;
using Minimalist.Effect.Animations;
using Minimalist.Effect.BloomIntensity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevShaderTest : MonoBehaviour
{

    [SerializeField] private BWEffectManager _bwManager;
    // Start is called before the first frame update
    [SerializeField] private CameraEffectControls _cmEffControl;
    [SerializeField] private AnimationCurve _animCurve;
    [SerializeField] private float time;
    [SerializeField] private GameObject _player;
    [SerializeField] private List<VfxAsset> _effects;
    private float effDelay = 1f;
    private float effTimeout = 0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _bwManager.SwapMode();
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            _cmEffControl.SetBloom(_cmEffControl.GetCurrentBloom() + 0.1f);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            _cmEffControl.SetBloom(_cmEffControl.GetCurrentBloom() - 0.1f);

        }

        if (Input.GetKey(KeyCode.S))
        {
            _cmEffControl.SetBloom(
                _cmEffControl.GetCurrentBloom() == 4.0f ? 0.0f : 4.0f,
                time, _animCurve);
        }
        if(effTimeout>0f) effTimeout -= 1f * Time.deltaTime;
        if (Input.GetKey(KeyCode.B))
        {
            if(effTimeout <= 0f)
            {
                effTimeout += effDelay;
                VfxAsset effect = _effects[Random.Range(0, _effects.Count)];
                VfxManager.Instance.CreateEffect(effect, _player.transform.position);
            }
        }
    }

    

    
}
