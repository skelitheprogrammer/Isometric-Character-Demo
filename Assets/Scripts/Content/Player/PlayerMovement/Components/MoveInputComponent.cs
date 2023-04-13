using UnityEngine;

namespace Content.Player.PlayerMovement.Components
{
    public struct MoveInputComponent
    {
        public Vector2 MoveInput;
        public Vector3 WorldMoveInput => new Vector3(MoveInput.x, 0, MoveInput.y);
        
    }
}