using System;
using UnityEngine;

namespace GameLogic.Player
{
    public class PlayerState : MonoBehaviour
    {
        public event Action<(int health, int maxHealth)> HealthParamsChanged;

        [SerializeField] private ABC_StateManager _stateManager;

        private int health;
        private int maxHealth;

        public ABC_StateManager StateManager => _stateManager;

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
            Health = (int)_stateManager.currentHealth;
            MaxHealth = (int)_stateManager.currentMaxHealth;
        }
    }
}