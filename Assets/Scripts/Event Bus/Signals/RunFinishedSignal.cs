using GameLogic;

namespace EventBus
{
    public class RunFinishedSignal
    {
        public readonly RunEndingStatus RunEndingStatus;

        public RunFinishedSignal(RunEndingStatus runEndingStatus)
        {
            RunEndingStatus = runEndingStatus;
        }
    }
}