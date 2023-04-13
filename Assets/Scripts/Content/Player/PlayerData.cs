using Content.Player.PlayerCamera;
using Content.Player.PlayerMovement;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Content.Player
{
    [CreateAssetMenu(menuName = "Create PlayerData", fileName = "PlayerData", order = 0)]
    public class PlayerData : ScriptableObject
    {
        [field: SerializeField] public AssetReference PlayerPrefab { get; private set; }
        [field: SerializeField] public AssetReference CameraPrefab { get; private set; }

        [field: SerializeField] public PlayerMovementData MovementData { get; private set; }
        [field: SerializeField] public PlayerRotationData RotationData { get; private set; }
        
        [field: SerializeField] public PlayerCameraData CameraData { get; private set; }
    }
}