using System.Collections;

using UnityEngine;

namespace Minimalist.Audio.Music
{
    [RequireComponent(typeof(MusicLibrary))]
    public class MusicManager : MonoBehaviour
    {
        public MusicManager Instance { get; private set; }

        [SerializeField] private MusicLibrary _musicLibrary;

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

        public void PlayMusic(string trackName, float fadeDuration = 0.5f)
        {
            StartCoroutine(AnimateMusicCrossFade(_musicLibrary.GetClipFromName(trackName), fadeDuration));
        }


        private IEnumerator AnimateMusicCrossFade(AudioClip nextTrack, float fadeDuration = 0.5f)
        {
            float percent = 0; // Used as intermediate variable for lerping

            while (percent < 1)
            {
                percent += Time.deltaTime * 1 / fadeDuration;
                _musicSource.volume = Mathf.Lerp(1f, 0, percent);

                yield return null;
            }

            _musicSource.clip = nextTrack;
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
