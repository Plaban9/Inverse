using UnityEngine;

namespace Minimalist.Audio.Music
{
    [System.Serializable]
    public class MusicTrack
    {
        public string name;
        public MusicType musicType;
        public AudioClip audioClip;
        public bool shouldLoop;
    }
}
