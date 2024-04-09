using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustEffectHack : MonoBehaviour
{
    [SerializeField] private JumpEffectHack _jumpEffectHack;

    public void AlertObserver(string message)
    {
        if (message.Equals("animationEnded"))
        {
            _jumpEffectHack.AlertObserver(message);
        }
    }
}
