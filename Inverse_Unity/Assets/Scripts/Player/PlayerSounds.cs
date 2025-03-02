using Minimalist.Audio;
using Minimalist.Audio.Music;
using Minimalist.Audio.Sound;
using Minimalist.Interfaces;
using Minimalist.Level;
using Minimalist.Manager;

using UnityEngine;

namespace Minimalist.Player
{
    public class PlayerSounds : MonoBehaviour, ILevelListener<LevelType>
    {
        private MyPlayerInput inputs;

        private void Awake()
        {
            inputs = GetComponent<MyPlayerInput>();
        }

        private void OnEnable()
        {
            inputs.OnDie += () => AudioManager.PlaySFX3D(SoundType.Player_Death, transform.position);
        }

        private void OnDisable()
        {
            LevelManager.Instance.RealmManager.RemoveListener(this);
        }

        private void Start()
        {
            LevelManager.Instance.RealmManager.AddListener(this);
            AudioManager.PlayMusic(MusicType.Gameplay);
            AudioManager.PlaySFX3D(SoundType.Player_Spawn, transform.position);
        }

        private void Update()
        {
            if (inputs.IsJumped)
            {
                AudioManager.PlaySFX3D(SoundType.Player_Jump, transform.position);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Death"))
            {
                AudioManager.PlaySFX3D(SoundType.Player_Death, transform.position);
            }
        }

        public void OnNotify(LevelType enums)
        {
            AudioManager.PlaySFX3D(SoundType.Gameplay_RealmChange, transform.position);
        }
    }
}