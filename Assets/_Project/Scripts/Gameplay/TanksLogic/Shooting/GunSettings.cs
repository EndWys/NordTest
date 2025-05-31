using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.TanksLogic.Shooting
{
    [CreateAssetMenu(menuName = "Game/TankGunSettings", fileName = "TankGunSettings")]
    public class GunSettings : ScriptableObject
    {
        [Header("Gun Settings")]
        public float BulletSpeed = 10f;
        public float FireCooldown = 0.5f;
    }
}