namespace GameLogic
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