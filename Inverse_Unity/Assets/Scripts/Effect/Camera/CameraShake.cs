using Cinemachine;

using System.Collections;
using System.Collections.Generic;
using System.Threading;

using UnityEngine;

namespace Minimalist.Effect.CameraShake
{
    public class CameraShake : MonoBehaviour
    {
        private CinemachineVirtualCamera _cinemachineVirtualCamera;
        [SerializeField] private float _shakeIntensity = 1f;
        [SerializeField] private float _resetIntensity = 0f;
        [SerializeField] private float _shakeTime = 1f;

        private float _timer;

        private CinemachineBasicMultiChannelPerlin _cinemachineNoise;

        private void Awake()
        {
            _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        }

        private void Start()
        {
            StopShake();
        }

        public void ShakeCamera()
        {
            if (_cinemachineVirtualCamera == null)
                return;

            var noise = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            noise.m_AmplitudeGain = _shakeIntensity;
            _timer = _shakeTime;
        }

        public void StopShake()
        {
            if (_cinemachineVirtualCamera == null)
                return;

            var noise = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            noise.m_AmplitudeGain = _resetIntensity;
            _timer = 0f;
        }

        public void HandleTimer()
        {
            if (_timer > 0)
            {
                _timer -= Time.deltaTime;

                if (_timer < 0)
                {
                    StopShake();
                }
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ShakeCamera();
            }

            HandleTimer();
        }
    }
}