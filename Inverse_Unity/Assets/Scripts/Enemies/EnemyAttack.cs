using Minimalist.Level;
using Minimalist.Manager;
using Minimalist.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyAttack : MonoBehaviour
{
    [HideInInspector] public float attackInterval;
    public event System.Action OnAttack;

    [SerializeField] private bool canDamageOnLightRealm;
    [SerializeField] private bool canDamageOnDarkRealm;
    [SerializeField] private float currentAttackTime;

    private void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!canDamageOnDarkRealm && !canDamageOnLightRealm)
            return;

        if (collision.TryGetComponent(out PlayerMovements movements))
        {
            currentAttackTime += Time.deltaTime;

            if (currentAttackTime > attackInterval)
            {
                currentAttackTime = 0;
                var currentRealm = LevelManager.Instance.RealmManager.GetCurrentLevelType();
                if (currentRealm == LevelType.Dark && canDamageOnDarkRealm ||
                    currentRealm == LevelType.Light && canDamageOnLightRealm)
                {
                    OnAttack?.Invoke();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        currentAttackTime = 0;
    }
}
