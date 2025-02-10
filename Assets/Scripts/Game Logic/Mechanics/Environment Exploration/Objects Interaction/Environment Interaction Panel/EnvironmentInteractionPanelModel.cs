using UnityEngine;
using UnityEngine.Localization;

namespace GameLogic.EnvironmentExploration
{
    public class EnvironmentInteractionPanelModel
    {
        public readonly LocalizedString InteractiveObjectName;
        public readonly LocalizedString InteractionDescription;
        public readonly Vector3 ViewWorldPosition;

        public EnvironmentInteractionPanelModel(LocalizedString interactiveObjectName, LocalizedString interactionDescription, Vector3 viewWorldPosition)
        {
            InteractiveObjectName = interactiveObjectName;
            InteractionDescription = interactionDescription;
            ViewWorldPosition = viewWorldPosition;
        }
    }
}