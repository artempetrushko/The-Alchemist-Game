using System;

namespace EventBus
{
    public class PlayerStateChangedSignal
    {
        public readonly Action<ABC_StateManager> ChangePlayerState;

        public PlayerStateChangedSignal(Action<ABC_StateManager> action) 
        {
            ChangePlayerState = action; 
        }
    }
}