using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minimalist.Utilities
{
    public class MovingPlatform : ObjectMovement
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.CompareTag("Player"))
            {
                collision.transform.SetParent(transform, true);
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.transform.CompareTag("Player"))
            {
                collision.transform.SetParent(null, true);
            }
        }
    }
}