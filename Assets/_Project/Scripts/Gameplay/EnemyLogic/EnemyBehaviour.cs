using Assets._Project.Scripts.ObjectPoolSytem;
using Assets._Project.Scripts.SaveSystem;
using System;

namespace Assets._Project.Scripts.Gameplay.EnemyLogic
{
    public abstract class EnemyBehaviour : PoolObject, ITankSave
    {
        public event Action<EnemyBehaviour> OnHit;
        public abstract void Init();
        public abstract TankSaveData GetSaveData();

        protected void TakeHit()
        {
            OnHit?.Invoke(this);
        }
    }
}