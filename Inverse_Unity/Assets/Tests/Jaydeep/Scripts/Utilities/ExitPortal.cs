using DG.Tweening;
using Minimalist.Audio;
using Minimalist.Audio.Sound;
using Minimalist.Manager;
using Minimalist.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minimalist.Utilities
{
    public class ExitPortal : MonoBehaviour
    {
        [SerializeField] private string nextLevelName;

        public event System.Action OnLevelCompleted;

        [ContextMenu("Skip Level")]
        private void LoadNextLevel()
        {
            AudioManager.PlaySFX3D(SoundType.Gameplay_LevelComplete, transform.position);
            OnLevelCompleted?.Invoke();
            SceneManager.Instance.LoadScene(nextLevelName, "CrossFade");
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<MyPlayerInput>(out var player))
            {
                AnimatePlayer(player);
                LoadNextLevel();
            }
        }

        private static void AnimatePlayer(MyPlayerInput player)
        {
            var rb = player.GetComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.velocity = Vector3.zero;
            player.enabled = false;
            player.transform.DOScale(Vector3.zero, .5f);
        }
    }
}