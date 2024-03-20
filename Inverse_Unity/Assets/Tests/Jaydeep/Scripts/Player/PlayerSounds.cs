using Minimalist.Audio;
using Minimalist.Audio.Music;
using Minimalist.Audio.Sound;
using Minimalist.Interfaces;
using Minimalist.Level;
using Minimalist.Manager;
using Minimalist.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minimilist.Player
{
    public class PlayerSounds : MonoBehaviour, ILevelListener<LevelType>
    {
        private PlayerMovements movements;

        private void Awake()
        {
            movements = GetComponent<PlayerMovements>();
        }

        private void OnEnable()
        {
            
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
            if (movements.IsPlayerJumped)
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
            // Realm Change Sound is in PlayerSound script bcz player is responsible/required to change the realm.
            AudioManager.PlaySFX(SoundType.Gameplay_RealmChange);
        }
    }
}