namespace GameLogic.EnvironmentExploration
{
    public class ObjectInteractionStartedSignal
    {
        public readonly InteractiveObject InteractiveObject;

        public ObjectInteractionStartedSignal(InteractiveObject interactiveObject)
        {
            InteractiveObject = interactiveObject;
        }
    }
}