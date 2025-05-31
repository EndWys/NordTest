using Assets._Project.Scripts.ObjectPoolSytem;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.EnemyLogic
{
    public class EnemyPool : GenericObjectPool<EnemyBehaviour>
    {
        [SerializeField] private EnemyBehaviour _enemyPrefab;

        protected override bool _collectionCheck => true;
        protected override int _defaultCapacity => 10;

        protected override EnemyBehaviour CratePoolObject()
        {
            EnemyBehaviour enemy = Instantiate(_enemyPrefab);
            return enemy;
        }
    }
}