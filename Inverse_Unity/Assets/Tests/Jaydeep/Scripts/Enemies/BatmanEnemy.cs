using Minimalist.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatmanEnemy : Enemy
{
    protected override void ActivateDarkState()
    {
        _canMove = true;
    }

    protected override void ActivateLightState()
    {
        _canMove = true;
    }

    protected override void HandleIdleState()
    {
        patrolling.StartMoving();
    }
}
