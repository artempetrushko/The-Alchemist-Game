namespace GameLogic.EnvironmentExploration
{
    public interface IInteractableObjectHandler
    {
        void HandleInteraction<T>(T interactiveObject) where T : InteractiveObject;
    }
}