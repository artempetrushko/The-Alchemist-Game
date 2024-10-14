using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace GameLogic
{
    public class GameBootstrap : MonoBehaviour
    {
        private GameManager _gameManager;

        [Inject]
        public void Construct(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        private void Start()
        {
            _gameManager.StartGameAsync().Forget();
        }
    }
}