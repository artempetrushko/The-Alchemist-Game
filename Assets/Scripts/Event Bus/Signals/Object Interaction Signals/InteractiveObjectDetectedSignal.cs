using GameLogic.EnvironmentExploration;

namespace EventBus
{
    public class InteractiveObjectDetectedSignal
	{
		public readonly InteractiveObject InteractiveObject;

		public InteractiveObjectDetectedSignal(InteractiveObject interactiveObject)
		{
			InteractiveObject = interactiveObject;
		}
	}
}