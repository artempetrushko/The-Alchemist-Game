using System;
using Zenject;

namespace GameLogic
{
    public class HealthCounterPresenter : IDisposable
    {
        private HealthCounterConfig _healthCounterConfig;
        private HealthCounterView _healthCounterView;
        private SignalBus _signalBus;
        private HealthBarState _currentHealthBarState;

        public HealthCounterPresenter(HealthCounterConfig healthCounterConfig, HealthCounterView healthCounterView, SignalBus signalBus)
        {
            _healthCounterConfig = healthCounterConfig;
            _healthCounterView = healthCounterView;
            _signalBus = signalBus;

            _signalBus.Subscribe<PlayerHealthChangedSignal>(OnPlayerHealthChanged);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<PlayerHealthChangedSignal>(OnPlayerHealthChanged);
        }

        private void SetCurrentHealthBarState(int health)
        {
            foreach (var healthBarState in _healthCounterConfig.HealthBarStates)
            {
                if (health >= healthBarState.MinHealthPercentage && health <= healthBarState.MaxHealthPercentage)
                {
                    _currentHealthBarState = healthBarState;
                    break;
                }
            }
        }

        private void OnPlayerHealthChanged(PlayerHealthChangedSignal signal)
        {
            _healthCounterView.SetHealthCounterText($"{signal.Health}/{signal.MaxHealth}");

            if (signal.Health < _currentHealthBarState.MinHealthPercentage || signal.Health > _currentHealthBarState.MaxHealthPercentage)
            {
                SetCurrentHealthBarState(signal.Health);
            }
            _healthCounterView.SetHealthBarSprite(_currentHealthBarState.HealthBarSprite);
            _healthCounterView.SetHealthBarFillingAreaSprite(_currentHealthBarState.HealthBarFillingAreaSprite);
            _healthCounterView.SetHealthBarFillingAreaFillAmount((float)signal.Health / signal.MaxHealth);
        }
    }
}