using Minimalist.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WereWolfEnemy : Enemy
{
    [Header("Colliders")]
    [SerializeField] private Collider2D idleCollider;
    [SerializeField] private Collider2D alertCollider;
    [SerializeField] private Collider2D chaseCollider;

    protected override void Update()
    {
        base.Update();
        HandleColliders();
    }

    private void HandleColliders()
    {
        switch (state)
        {
            case PatrolStates.Idle:
                alertCollider.enabled = false;
                chaseCollider.enabled = false;
                idleCollider.enabled = true;
                break;
            case PatrolStates.Chase:
                idleCollider.enabled = false;
                alertCollider.enabled = false;
                chaseCollider.enabled = true;
                break;

            case PatrolStates.Patrol:
            case PatrolStates.Alert:
                idleCollider.enabled = false;
                chaseCollider.enabled = false;
                alertCollider.enabled = true;
                break;
        }
    }

    protected override void ActivateDarkState()
    {
        _canMove = true;
        rb.gravityScale = isRealmIndependentEnemy ? 1 : isLightRealmEnemy ? 0 : 1;
        state = PatrolStates.Idle;
        rb.mass = 10;
        animator.speed = 1;
        gameObject.layer = 0;
    }

    protected override void ActivateLightState()
    {
        _canMove = false;
        rb.velocity = Vector2.zero;
        rb.gravityScale = isRealmIndependentEnemy ? 1 : isLightRealmEnemy ? 1 : 0;
        rb.mass = 100 * 100;
        animator.speed = 0;
        gameObject.layer = 9;
    }
}
