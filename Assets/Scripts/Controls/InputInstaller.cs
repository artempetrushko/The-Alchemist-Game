using UnityEngine;
using Zenject;

namespace Controls
{
    public class InputInstaller : MonoInstaller
    {
        [SerializeField] private InputConfig _config;

        public override void InstallBindings()
        {
            Container.Bind<InputManager>().AsSingle().NonLazy();
            Container.Bind<InputConfig>().FromScriptableObject(_config).AsSingle().NonLazy();
        }
    }
}