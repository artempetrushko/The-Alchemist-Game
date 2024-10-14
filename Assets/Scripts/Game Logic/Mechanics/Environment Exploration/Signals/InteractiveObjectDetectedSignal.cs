using GameLogic.EnvironmentExploration;

namespace GameLogic.Player
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