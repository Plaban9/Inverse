using Cinemachine;
using Cinemachine.Editor;

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

        [SerializeField] private CinemachineBasicMultiChannelPerlin _cinemachineNoise;

        public static CameraShake Instance { get; private set; }

        private void Awake()
        {
            _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        }

        private void Start()
        {
            if (Instance == null)
            {
                Instance = this;
            }

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

        public void ShakeCamera(float intensity, float duration)
        {
            if (_cinemachineVirtualCamera == null)
                return;

            var noise = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            noise.m_AmplitudeGain = intensity;
            _timer = duration;
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