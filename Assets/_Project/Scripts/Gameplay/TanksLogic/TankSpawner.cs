using Assets._Project.Scripts.SaveSystem;
using System.Collections;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.TanksLogic
{
    public abstract class TankSpawner<TData> : MonoBehaviour, ISaveLoad<TData> where TData : class, ISaveData
    {
        protected SavingService _savingService;

        private float _autoSaveInterval = 2f;

        protected abstract string _saveFileName { get; }

        public virtual void Init()
        {
            _savingService = new SavingService(_saveFileName);

            if (!TryToLoadSave())
                StartCoroutine(Spawn());

            StartCoroutine(AutoSave());
        }

        protected abstract IEnumerator Spawn();

        private bool TryToLoadSave()
        {
            var save = _savingService.Load<TData>();

            if (save == null)
                return false;

            Load(save);
            return true;
        }

        public IEnumerator AutoSave()
        {
            while (true)
            {
                yield return new WaitForSeconds(_autoSaveInterval);
                Save();
            }
        }

        public abstract void Save();
        public abstract void Load(TData data);
    }
}