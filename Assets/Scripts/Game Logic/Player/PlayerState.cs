using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public event Action<(int health, int maxHealth)> HealthParamsChanged;

    [SerializeField]
    private ABC_StateManager stateManager;
    private int health;
    private int maxHealth;

    private int Health
    {
        get => health;
        set
        {
            if (health != value)
            {
                health = value;
                HealthParamsChanged?.Invoke((Health, MaxHealth));
            }
        }
    }

    private int MaxHealth
    {
        get => maxHealth;
        set
        {
            if (maxHealth != value)
            {
                maxHealth = value;
                HealthParamsChanged?.Invoke((Health, MaxHealth));
            }
        }
    }

    private void Update()
    {
        Health = (int)stateManager.currentHealth;
        MaxHealth = (int)stateManager.currentMaxHealth;
    }
}
