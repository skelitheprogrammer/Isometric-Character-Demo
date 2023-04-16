using UnityEngine;

namespace Content.Player.PlayerCamera.Components
{
    [System.Serializable]
    public struct PlayerCameraData
    {
        [HideInInspector] public Vector3 CameraOffset;
        public float CameraTravelSpeed;
    }
}