using Assets._Project.Scripts.Extansions;
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

        private readonly List<EnemyBehaviour> _activeEnemies = new();

        public void Init()
        {
            _enemyPool.CreatePool();

            SpawnAllEnemies();
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

            if (_activeEnemies.Count == 0)
            {
                StartCoroutine(RespawnAllEnemiesAfterDelay());
            }
        }

        private IEnumerator RespawnAllEnemiesAfterDelay()
        {
            yield return new WaitForSeconds(_respawnDelay);
            SpawnAllEnemies();
        }
    }
}