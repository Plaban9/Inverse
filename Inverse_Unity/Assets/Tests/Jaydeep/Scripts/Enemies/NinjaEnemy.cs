using Minimalist.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaEnemy : Enemy
{
    protected override void ActivateDarkState()
    {
        _canMove = false;
        rb.velocity = Vector2.zero;
        rb.gravityScale = isRealmIndependentEnemy ? 1 : isLightRealmEnemy ? 0 : 1;
        rb.mass = 100 * 100;
        animator.speed = 0;
    }

    protected override void ActivateLightState()
    {
        _canMove = true;
        rb.gravityScale = isRealmIndependentEnemy ? 1 : isLightRealmEnemy ? 1 : 0;
        state = PatrolStates.Idle;
        rb.mass = 10;
        animator.speed = 1;
    }
}
