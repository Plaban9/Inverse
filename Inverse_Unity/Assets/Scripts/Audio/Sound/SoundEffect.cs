using System;

using UnityEngine;

namespace Minimalist.Audio.Sound
{
    [Serializable]
    public struct SoundEffect
    {
        public string groupID;
        public AudioClip[] clips;
    }
}