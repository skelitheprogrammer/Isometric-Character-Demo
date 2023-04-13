using System;

namespace Common.Input
{
    public class InputStateControlService : IDisposable
    {
        private readonly PlayerActions _playerActions;

        public InputStateControlService(PlayerActions playerActions)
        {
            _playerActions = playerActions;
        }

        public void Enable()
        {
            _playerActions.Enable();
        }

        public void Disable()
        {
            _playerActions.Disable();
        }

        public void Dispose()
        {
            _playerActions?.Dispose();
        }
    }
}
