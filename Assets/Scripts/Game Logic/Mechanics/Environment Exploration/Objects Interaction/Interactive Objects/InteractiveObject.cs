using UnityEngine;
using UnityEngine.Localization;

namespace GameLogic.EnvironmentExploration
{
    public abstract class InteractiveObject : MonoBehaviour
    {
        [SerializeField] private LocalizedString _name;
        [SerializeField] private LocalizedString _interactionDescription;

        public LocalizedString Name => _name;
        public LocalizedString InteractionDescription => _interactionDescription;

        public abstract void Interact();
    }
}