using UnityEngine;

namespace Minimalist.Audio.Music
{
    public class MusicLibrary : MonoBehaviour
    {
        [SerializeField] private MusicTrack[] musicTracks;

        public AudioClip GetClipFromName(string name)
        {
            foreach (var track in musicTracks)
            {
                if (track.trackName.Equals(name))
                {
                    return track.audioClip;
                }
            }

            return null;
        }
    }
}
