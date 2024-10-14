using UnityEngine;
using UnityEngine.Localization;

namespace GameLogic.EnvironmentExploration
{
    public class EnvironmentInteractionModel
    {
        public readonly LocalizedString InteractiveObjectName;
        public readonly LocalizedString InteractionDescription;
        public readonly Vector3 ViewWorldPosition;

        public EnvironmentInteractionModel(LocalizedString interactiveObjectName, LocalizedString interactionDescription, Vector3 viewWorldPosition)
        {
            InteractiveObjectName = interactiveObjectName;
            InteractionDescription = interactionDescription;
            ViewWorldPosition = viewWorldPosition;
        }
    }
}