using System;

using UnityEngine;

namespace Minimalist.Audio.Music
{
    [Serializable]
    public struct MusicTrack
    {
        public string trackName;
        public AudioClip audioClip;
    }
}
