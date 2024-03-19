using UnityEngine;

namespace Minimalist.Audio.Sound
{
    internal class SoundManager : MonoBehaviour
    {
        internal static SoundManager Instance { get; private set; }

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

        // For UI sounds (mostly)
        internal void PlaySound2D(AudioClip audioClip)
        {
            _sfxSource2D.PlayOneShot(audioClip);
        }

        // When not in List
        internal void PlaySound3D(AudioClip audioClip, Vector3 position)
        {
            if (audioClip != null)
            {
                AudioSource.PlayClipAtPoint(audioClip, position);
            }
        }
    }
}
