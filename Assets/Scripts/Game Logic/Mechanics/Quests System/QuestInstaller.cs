using UnityEngine;
using Zenject;

namespace GameLogic.QuestSystem
{
    public class QuestInstaller : MonoInstaller
    {
        [SerializeField] private QuestProgressView _questProgressView;

        public override void InstallBindings()
        {
            Container.Bind<QuestPresenter>().AsSingle().NonLazy();
            Container.Bind<QuestProgressView>().FromInstance(_questProgressView).AsSingle().NonLazy();
        }
    }
}