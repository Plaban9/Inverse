
using UnityEngine;

namespace Minimalist.Audio.Sound
{
    public class SoundLibrary : MonoBehaviour
    {
        [SerializeField] private SoundEffect[] _soundEffects;

        public AudioClip GetClipFromName(string name)
        {
            foreach (var soundEffect in _soundEffects)
            {
                if (soundEffect.groupID.Equals(name))
                {
                    return soundEffect.clips[Random.Range(0, soundEffect.clips.Length)];
                }
            }

            return null;
        }
    }
}
