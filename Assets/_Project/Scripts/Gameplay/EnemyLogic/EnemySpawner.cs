using Assets._Project.Scripts.Extansions;
using Assets._Project.Scripts.Gameplay.TanksLogic;
using Assets._Project.Scripts.SaveSystem;
using Assets._Project.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.EnemyLogic
{
    public class EnemySpawner : TankSpawner<EnemySpawnerSaveData>
    {
        [SerializeField] private EnemyPool _enemyPool;

        [Header("Spawn Zone Settings")]
        [SerializeField] private Collider2D _spawnZone;
        [SerializeField] private LayerMask _tankLayerMask;

        [SerializeField] private float _spawnCheckRadius = 1f;

        [Header("Respawn Settings")]
        [SerializeField] private int _enemyCount = 5;
        [SerializeField] private float _respawnDelay = 1f;

        private readonly List<EnemyBehaviour> _activeEnemies = new();

        protected override string _saveFileName => "enemy_spawner";

        public override void Init()
        {
            _enemyPool.CreatePool();
            
            base.Init();
        }

        public override void Load(EnemySpawnerSaveData save)
        {
            StartCoroutine(LoadEnemiesFromSaveRoutine(save));
        }

        private IEnumerator LoadEnemiesFromSaveRoutine(EnemySpawnerSaveData save)
        {
            foreach (var data in save.EnemySaveDatas)
            {
                SpawnSingleEnemy(data.Position, data.Rotation);
                yield return null;
            }

            GameUI.Instance.SetEnemyCount(_activeEnemies.Count);
            GameUI.Instance.HideCenterPanel();
        }

        private void Despawn(EnemyBehaviour enemy)
        {
            enemy.OnHit -= Despawn;

            _enemyPool.ReleaseObject(enemy);
            _activeEnemies.Remove(enemy);

            Save();

            GameUI.Instance.SetEnemyCount(_activeEnemies.Count);

            if (TryToStartEnemiesRespawn())
                GameUI.Instance.ShowWinScreen();
        }

        public override void Save()
        {
            var savedData = new TankSaveData[_activeEnemies.Count];
            for (int i = 0; i < _activeEnemies.Count; i++)
            {
                var enemy = _activeEnemies[i];
                savedData[i] = new TankSaveData(enemy.transform.position, enemy.transform.rotation);
            }

            var save = new EnemySpawnerSaveData(savedData);
            _savingService.Save(save);
        }

        private Vector2? GetValidSpawnPoint()
        {
            Vector2 point = _spawnZone.GetRandomPointOnBounds();

            Collider2D hit = Physics2D.OverlapCircle(point, _spawnCheckRadius, _tankLayerMask);
            if (hit == null)
            {
                return point;
            }

            return null;
        }

        private bool TryToStartEnemiesRespawn()
        {
            bool allEnemiesDestroyed = _activeEnemies.Count == 0;

            if (allEnemiesDestroyed)
                StartCoroutine(RespawnAllEnemiesAfterDelay());

            return allEnemiesDestroyed;
        }

        private IEnumerator RespawnAllEnemiesAfterDelay()
        {
            yield return new WaitForSeconds(_respawnDelay);
            yield return Spawn();
        }

        protected override IEnumerator Spawn()
        {
            int spawned = 0;

            while (spawned < _enemyCount)
            {
                Vector2? spawnPoint = GetValidSpawnPoint();
                if (spawnPoint.HasValue)
                {
                    SpawnSingleEnemy(spawnPoint.Value, Quaternion.identity);
                    spawned++;
                }

                yield return null;
            }

            GameUI.Instance.SetEnemyCount(_activeEnemies.Count);
            GameUI.Instance.HideCenterPanel();
        }

        private void SpawnSingleEnemy(Vector2 position, Quaternion rotation)
        {
            EnemyBehaviour enemy = _enemyPool.GetObject();

            enemy.transform.position = position;
            enemy.transform.rotation = rotation;

            enemy.OnHit += Despawn;
            _activeEnemies.Add(enemy);

            enemy.Init();
        }
    }
}