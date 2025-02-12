using EventBus;
using UnityEngine;
using Zenject;

namespace GameLogic.Player
{
    public class PlayerState : MonoBehaviour
    {
        [SerializeField] private ABC_StateManager _stateManager;

        private SignalBus _signalBus;
        private int _health;
        private int _maxHealth;

        public ABC_StateManager StateManager => _stateManager;

        private int Health
        {
            get => _health;
            set
            {
                if (_health != value)
                {
                    _health = value;
                    _signalBus.Fire(new PlayerHealthChangedSignal(_health, _maxHealth));
                }
            }
        }

        private int MaxHealth
        {
            get => _maxHealth;
            set
            {
                if (_maxHealth != value)
                {
                    _maxHealth = value;
                    _signalBus.Fire(new PlayerHealthChangedSignal(_health, _maxHealth));
                }
            }
        }

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void Update()
        {
            Health = (int)_stateManager.currentHealth;
            MaxHealth = (int)_stateManager.currentMaxHealth;
        }
    }
}