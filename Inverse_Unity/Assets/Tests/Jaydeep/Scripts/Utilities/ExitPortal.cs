using Minimalist.Manager;
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
            if (collision.CompareTag("Player"))
            {
                SceneManager.Instance.LoadScene(nextLevelName, "CrossFade");
            }
        }
    }
}