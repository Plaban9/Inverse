using System.Collections;

using UnityEngine;

namespace Minimalist.Audio.Music
{
    internal class MusicManager : MonoBehaviour
    {
        internal static MusicManager Instance { get; private set; }

        [SerializeField] private AudioSource _musicSource;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        internal void PlayMusic(AudioClip audioClip, float fadeDuration = 0.5f, bool loop = true)
        {
            StartCoroutine(AnimateMusicCrossFade(audioClip, fadeDuration, loop));
        }


        private IEnumerator AnimateMusicCrossFade(AudioClip nextTrack, float fadeDuration = 0.5f, bool loop = true)
        {
            float percent = 0; // Used as intermediate variable for lerping

            while (percent < 1)
            {
                percent += Time.deltaTime * 1 / fadeDuration;
                _musicSource.volume = Mathf.Lerp(1f, 0, percent);

                yield return null;
            }

            _musicSource.clip = nextTrack;
            _musicSource.loop = loop;
            _musicSource.Play();

            percent = 0;

            while (percent < 1)
            {
                percent += Time.deltaTime * 1 / fadeDuration;
                _musicSource.volume = Mathf.Lerp(0f, 1f, percent);

                yield return null;
            }
        }
    }
}
