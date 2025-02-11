using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Controls
{
    public class InputInstaller : MonoInstaller
    {
        [SerializeField] private InputConfig _config;
        [SerializeField] private PlayerInput _playerInput;

        public override void InstallBindings()
        {
            Container.Bind<InputManager>().AsSingle().NonLazy();
            Container.Bind<InputConfig>().FromScriptableObject(_config).AsSingle().NonLazy();
            Container.Bind<PlayerInput>().FromInstance(_playerInput).AsSingle().NonLazy();
        }
    }
}