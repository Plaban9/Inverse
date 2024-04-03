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

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<MyPlayerInput>(out var playerInput))
            {
                playerInput.enabled = false;
                AudioManager.PlaySFX3D(SoundType.Gameplay_LevelComplete, transform.position);
                OnLevelCompleted?.Invoke();
                SceneManager.Instance.LoadScene(nextLevelName, "CrossFade");
            }
        }
    }
}