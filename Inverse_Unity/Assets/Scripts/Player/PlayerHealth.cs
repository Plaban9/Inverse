using Minimalist.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 3f;
    [field: SerializeField] public float Health {  get; private set; }

    private MyPlayerInput player;

    private void Awake()
    {
        player = GetComponent<MyPlayerInput>();
    }

    private void OnEnable()
    {
        EnemyAttack.OnAnyAttack += OnAnyAttack;
    }

    private void OnDisable()
    {
        EnemyAttack.OnAnyAttack -= OnAnyAttack;
    }

    private void Start()
    {
        Health = maxHealth;
    }

    private void OnAnyAttack()
    {
        Health -= .5f;
        Health = Mathf.Clamp(Health, 0, maxHealth);

        Debug.Log($"Health: {Health}");

        if (Health <= 0)
        {
            player.Die();
            GetComponent<CapsuleCollider2D>().size = Vector2.one * .5f;
        }
    }
}
