using UnityEngine;

namespace Common.Utils.Components
{
    public struct RotationComponent
    {
        public Vector3 EulerRotation => Rotation.eulerAngles;
        public Quaternion Rotation;
    }
}