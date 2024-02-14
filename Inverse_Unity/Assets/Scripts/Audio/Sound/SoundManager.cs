using UnityEngine;

namespace Minimalist.Audio.Sound
{
    [RequireComponent(typeof(SoundLibrary))]
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance { get; private set; }

        [SerializeField] private SoundLibrary _sfxLibrary;

        [SerializeField] private AudioSource _sfxSource2D;

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

        private void Start()
        {
            if (_sfxLibrary == null)
            {
                _sfxLibrary = GetComponent<SoundLibrary>();
            }
        }

        // For UI sounds (mostly)
        public void PlaySound2D(string soundName)
        {
            _sfxSource2D.PlayOneShot(_sfxLibrary.GetClipFromName(soundName));
        }

        // When not in List
        public void PlaySound3D(AudioClip audioClip, Vector3 position)
        {
            if (audioClip != null)
            {
                AudioSource.PlayClipAtPoint(audioClip, position);
            }
        }

        public void PlaySound3D(string soundName, Vector3 position)
        {
            PlaySound3D(_sfxLibrary.GetClipFromName(soundName), position);
        }
    }
}
