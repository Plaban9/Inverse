using Managers.BWEffectManager;
using Managers.BWState;
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
    }

    

    
}
