using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.EnemyLogic
{
    public abstract class EnemyFactory : MonoBehaviour
    {
        public abstract void Init();

        public abstract EnemyBehaviour Spawn(Vector2 position, Quaternion rotation);

        public abstract void Despawn(EnemyBehaviour enemy);
    }
}