using Minimalist.Effect;
using Minimalist.Effect.Animations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpEffectHack : VfxController
{
    public override void AlertObserver(string message)
    {
        if (message.Equals("animationEnded"))
        {
            VfxManager.Instance.AddToInactiveEffect(this.gameObject);
        }
    }
}
