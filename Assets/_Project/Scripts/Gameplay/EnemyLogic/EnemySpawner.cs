using Assets._Project.Scripts.Extansions;
using Assets._Project.Scripts.SaveSystem;
using Assets._Project.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.EnemyLogic
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyPool _enemyPool;

        [Header("Spawn Zone Settings")]
        [SerializeField] private Collider2D _spawnZone;
        [SerializeField] private LayerMask _tankLayerMask;

        [SerializeField] private float _spawnCheckRadius = 1f;

        [Header("Respawn Settings")]
        [SerializeField] private int _enemyCount = 5;
        [SerializeField] private float _respawnDelay = 1f;

        private SavingService _savingService;

        private readonly List<EnemyBehaviour> _activeEnemies = new();

        private float _autoSaveInterval = 2f;

        public void Init()
        {
            _enemyPool.CreatePool();
            
            _savingService = new SavingService("enemy_spawner");

            var save = _savingService.Load<EnemySpawnerSaveData>();

            if (save != null && save.EnemySaveDatas.Length > 0)
                LoadSave(save);
            else
                SpawnAllEnemies();

            StartCoroutine(AutoSaveRoutine());
        }

        private void LoadSave(EnemySpawnerSaveData save)
        {
            StartCoroutine(LoadEnemiesFromSaveRoutine(save.EnemySaveDatas));
        }

        private IEnumerator LoadEnemiesFromSaveRoutine(TankSaveData[] savedEnemies)
        {
            foreach (var data in savedEnemies)
            {
                EnemyBehaviour enemy = _enemyPool.GetObject();

                enemy.transform.position = data.Position;
                enemy.transform.rotation = data.Rotation;
                enemy.gameObject.SetActive(true);

                enemy.OnHit += Despawn;
                _activeEnemies.Add(enemy);

                enemy.Init();

                yield return null;
            }

            GameUI.Instance.SetEnemyCount(_activeEnemies.Count);
            GameUI.Instance.HideCenterPanel();
        }

        private IEnumerator AutoSaveRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(_autoSaveInterval);
                Sava();
            }
        }

        private void Sava()
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

        private void SpawnAllEnemies()
        {
            StartCoroutine(SpawnEnemiesCoroutine(_enemyCount));
        }

        private IEnumerator SpawnEnemiesCoroutine(int count)
        {
            int spawned = 0;

            while (spawned < count)
            {
                Vector2? spawnPoint = GetValidSpawnPoint();
                if (spawnPoint.HasValue)
                {
                    EnemyBehaviour enemy = _enemyPool.GetObject();
                    enemy.transform.position = spawnPoint.Value;
                    enemy.gameObject.SetActive(true);

                    enemy.OnHit += Despawn;
                    _activeEnemies.Add(enemy);

                    enemy.Init();

                    spawned++;
                }

                yield return null;
            }


            GameUI.Instance.SetEnemyCount(spawned);
            GameUI.Instance.HideCenterPanel();
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

        private void Despawn(EnemyBehaviour enemy)
        {
            enemy.OnHit -= Despawn;
            _enemyPool.ReleaseObject(enemy);
            _activeEnemies.Remove(enemy);

            Sava();

            GameUI.Instance.SetEnemyCount(_activeEnemies.Count);

            if (_activeEnemies.Count == 0)
            {
                StartCoroutine(RespawnAllEnemiesAfterDelay());

                GameUI.Instance.ShowWinScreen();
            }
        }

        private IEnumerator RespawnAllEnemiesAfterDelay()
        {
            yield return new WaitForSeconds(_respawnDelay);
            SpawnAllEnemies();
        }
    }
}