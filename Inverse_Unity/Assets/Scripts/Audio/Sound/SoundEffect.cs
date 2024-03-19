using Minimalist.Audio.Music;

using UnityEngine;

namespace Minimalist.Audio.Sound
{
    [System.Serializable]
    public class SoundEffect
    {
        public string name;
        public SoundType soundType;
        public AudioClip[] clips;

        public AudioClip GetRandomClip()
        {
            if (clips?.Length > 0)
            {
                return clips[Random.Range(0, clips.Length)];
            }

            return null;
        }
    }
}