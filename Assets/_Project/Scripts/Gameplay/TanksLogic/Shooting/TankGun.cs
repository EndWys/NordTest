using Assets._Project.Scripts.Gameplay.TanksLogic.Bullets;
using Assets._Project.Scripts.Gameplay.TanksLogic.Control;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.TanksLogic.Shooting
{
    public class TankGun : MonoBehaviour
    {
        [SerializeField] private Transform _firePoint;
        [SerializeField] private BulletPool _bulletPool;

        [SerializeField] private GunSettings _gunSettings;

        private float _cooldownTimer;

        public void Init()
        {
            _cooldownTimer = 0;
            _bulletPool.CreatePool();
        }

        private void Update()
        {
            if (_cooldownTimer > 0f)
                _cooldownTimer -= Time.deltaTime;
        }

        public void TryToShoot(GunControlData gunData)
        {
            if (!gunData.Shoot || _cooldownTimer > 0f)
                return;

            var bullet = _bulletPool.GetObject();
            bullet.Init(_firePoint.position, _firePoint.up, _gunSettings.BulletSpeed);
            bullet.OnHit += OnBulletHit;

            _cooldownTimer = _gunSettings.FireCooldown;
        }

        private void OnBulletHit(Bullet bullet)
        {
            bullet.OnHit -= OnBulletHit;

            _bulletPool.ReleaseObject(bullet);
        }
    }
}