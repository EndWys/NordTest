using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.EnemyLogic
{
    public class DefaultEnemyFactory : EnemyFactory
    {
        /*
         * In the future, we’ll be able to create a different EnemyFactory with a new spawning mechanism 
         * or replace the abstract EnemyPool with a pool containing specific enemy types — for example, AI tanks equipped with gun.
         */

        [SerializeReference] protected EnemyPool _enemyPool;

        public override void Init()
        {
            _enemyPool.CreatePool();
        }

        public override EnemyBehaviour Spawn(Vector2 position, Quaternion rotation)
        {
            EnemyBehaviour enemy = _enemyPool.GetObject();

            enemy.transform.position = position;
            enemy.transform.rotation = rotation;

            enemy.Init();

            return enemy;
        }

        public override void Despawn(EnemyBehaviour enemy)
        {
            _enemyPool.ReleaseObject(enemy);
        }
    }
}