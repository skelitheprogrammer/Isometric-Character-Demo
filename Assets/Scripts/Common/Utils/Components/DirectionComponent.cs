using UnityEngine;

namespace Common.Utils.Components
{
    public struct DirectionComponent
    {
        public Vector3 ForwardDirection;
        public Vector3 RightDirection;
        public Vector3 UpDirection;

        public Vector3 CombinedDirection => ForwardDirection + RightDirection;
    }
}