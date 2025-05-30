using Assets._Project.Scripts.Gameplay.TanksLogic.Bullet;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.TanksLogic.Shooting
{
    public class TankGun : MonoBehaviour
    {
        [SerializeField] private Transform _firePoint;
        [SerializeField] private BulletPool _bulletPool;

        [SerializeField] private float _bulletSpeed = 10f;
        [SerializeField] private float _fireCooldown = 0.5f;

        private float _cooldownTimer;

        public void Init()
        {
            _bulletPool.CreatePool();
        }

        private void Update()
        {
            if (_cooldownTimer > 0f)
                _cooldownTimer -= Time.deltaTime;
        }

        public void TryToShoot(ShootData shootData)
        {
            if (!shootData.Shoot || _cooldownTimer > 0f)
                return;

            Debug.Log("Shoot");

            var bullet = _bulletPool.GetObject();
            bullet.Init(_firePoint.position, _firePoint.up, _bulletSpeed);

            _cooldownTimer = _fireCooldown;
        }
    }
}