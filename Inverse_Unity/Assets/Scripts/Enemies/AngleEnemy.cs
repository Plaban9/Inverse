using Minimalist.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleEnemy : Enemy
{
    #region Angle States
    protected override void HandleIdleState() 
    {
        patrolling.StopMoving();
    }
    #endregion

    protected override void ActivateDarkState()
    {
        _canMove = true;
        patrolling.StartMoving();
    }

    protected override void ActivateLightState()
    {
        _canMove = false;
        patrolling.StopMoving();
        rb.velocity = Vector3.zero;
    }
}
