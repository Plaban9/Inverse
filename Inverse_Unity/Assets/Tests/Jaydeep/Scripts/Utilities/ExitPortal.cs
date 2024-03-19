using Minimalist.Manager;
using Minimalist.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minimilist.Utilities
{
    public class ExitPortal : MonoBehaviour
    {
        [SerializeField] private string nextLevelName;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<MyPlayerInput>(out var playerInput))
            {
                playerInput.enabled = false;
                SceneManager.Instance.LoadScene(nextLevelName, "CrossFade");
            }
        }
    }
}