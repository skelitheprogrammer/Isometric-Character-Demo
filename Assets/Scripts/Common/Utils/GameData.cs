using Content.Player;
using UnityEngine;

namespace Common.Utils
{
    [CreateAssetMenu(menuName = "Create GameData", fileName = "GameData", order = 0)]
    public class GameData : ScriptableObject
    {
        [field: SerializeField] public PlayerData PlayerData { get; private set; }
    }
}