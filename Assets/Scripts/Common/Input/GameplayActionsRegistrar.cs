using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Common.Input
{
    public class GameplayActionsRegistrar : PlayerActions.IGameplayActions
    {
        public event Action<Vector2> Move;

        public GameplayActionsRegistrar(PlayerActions.GameplayActions gameplayActions)
        {
            gameplayActions.SetCallbacks(this);
        }
    
        public void OnMove(InputAction.CallbackContext context)
        {
            if (context.performed || context.canceled)
            {
                Vector2 value = context.ReadValue<Vector2>();
                Move?.Invoke(value);
            }
        }
    
    }
}
