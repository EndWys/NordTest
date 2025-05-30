using Assets._Project.Scripts.ObjectPoolSytem;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.TanksLogic.Bullet
{
    public class BulletPool : GenericObjectPool<Bullet>
    {
        [SerializeField] private Bullet _bulletPrefab;

        protected override bool _collectionCheck => false;
        protected override int _defaultCapacity => 20;

        protected override Bullet CratePoolObject()
        {
            var bullet = Instantiate(_bulletPrefab);
            bullet.SetPool(this);
            return bullet;
        }
    }
}