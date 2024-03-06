using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Minimalist.Effect.BloomIntensity
{
    [RequireComponent(typeof(Volume))]
    public class CameraEffectControls : MonoBehaviour
    {
        private Volume volume;
        private Bloom bloom;
        private bool transitioning;

        private void Awake()
        {
            volume = GetComponent<Volume>();
            volume.profile.TryGet(out Bloom bloomObj);
            bloom = bloomObj;
            transitioning = false;
        }

        /// <summary>
        /// Sets the value of bloom intensity without any transition. Use the other overloads for a smoother experience.
        /// </summary>
        /// <param name="final">Float value of the intensity</param>
        public void SetBloom(float final)
        {
            bloom.intensity.value = final;
        }

        /// <summary>
        /// Sets the value of bloom intensity with a linear interpolation over the time.
        /// </summary>
        /// <param name="final">Float value of the intensity</param>
        /// <param name="time">Time in Ms to reach the final value</param>
        public void SetBloom(float final, float time)
        {
            if (transitioning) return;
            transitioning = true;
            AnimationCurve animCurve = AnimationCurve.Linear(0f,0f,1f,1f);
            StartCoroutine(BloomChangeCoroutine(final, time, animCurve));
        }


        /// <summary>
        /// Sets the value of bloom intensity with a given interpolation over the time.
        /// </summary>
        /// <param name="final">Float value of the intensity</param>
        /// <param name="time">Time in Ms to reach the final value</param>
        /// <param name="animCurve">Function curve for the effect</param>
        public void SetBloom(float final, float time, AnimationCurve animCurve)
        {
            if (transitioning) return;
            transitioning = true;
            StartCoroutine(BloomChangeCoroutine(final, time, animCurve));
        }

        /// <summary>
        /// Returns the current intesity of Bloom effect
        /// </summary>
        /// <returns>Current intensity of bloom as a float</returns>
        public float GetCurrentBloom()
        {
            return bloom.intensity.value;
        }

        
        private IEnumerator BloomChangeCoroutine(
            float final,
            float time,
            AnimationCurve curve)
        {
            float initialValue = GetCurrentBloom();

            float transformValue = (final - initialValue);

            for (float t = 0f; t < 1f; t += 1 / time) { 
                bloom.intensity.value = initialValue + transformValue*curve.Evaluate(t);
                yield return null;
            }

            bloom.intensity.value = final;
            transitioning = false;
        }
    }
}