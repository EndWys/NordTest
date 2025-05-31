using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.TanksLogic.Scriptables
{
    [CreateAssetMenu(menuName = "Game/TankMovementSettings", fileName = "TankMovementSettings")]
    public class TankMovementSettings : ScriptableObject
    {
        [Header("Movement Settings")]
        public float MoveSpeed = 5f;
        public float RotationSpeed = 200f;
    }
}