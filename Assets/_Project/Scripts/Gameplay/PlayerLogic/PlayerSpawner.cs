using Assets._Project.Scripts.Extansions;
using Assets._Project.Scripts.SaveSystem;
using Assets._Project.Scripts.UI;
using System.Collections;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.PlayerLogic
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private PlayerBehaviour _player;

        [Header("Spawn Zone Settings")]
        [SerializeField] private Collider2D _spawnZone;

        [SerializeField] private float _spawnCheckRadius = 1f;
        [SerializeField] private LayerMask _tankLayer;

        private Vector2[] _cornerPoints;

        private SavingService _savingService;
        private float _autoSaveInterval = 2f;

        public void Init()
        {
            _savingService = new SavingService("player_spawner");

            TryToLoadPlayerData();
            StartCoroutine(AutoSaveRoutine());

            _cornerPoints = _spawnZone.GetCornerPoints();

            _player.Init();
            _player.OnHit += Despawn;
        }

        private void TryToLoadPlayerData()
        {
            var save = _savingService.Load<TankSaveData>();

            if (save == null)
                return;

            _player.transform.position = save.Position;
            _player.transform.rotation = save.Rotation;
        }


        private IEnumerator AutoSaveRoutine()
        {
            while (true) {

                yield return new WaitForSeconds(_autoSaveInterval);
                _savingService.Save(new TankSaveData(_player.transform.position, _player.transform.rotation));
            }
        }

        private void Despawn()
        {
            GameUI.Instance.ShowGameOverScreen();

            _player.gameObject.SetActive(false);
            StartCoroutine(RespawnPlayerCoroutine());
        }

        private IEnumerator RespawnPlayerCoroutine()
        {
            yield return new WaitForSeconds(1f);
            yield return SpawnWithRetryCoroutine();

            GameUI.Instance.HideCenterPanel();
        }

        private IEnumerator SpawnWithRetryCoroutine()
        {
            while (true)
            {
                foreach (Vector2 corner in _cornerPoints)
                {
                    if (Physics2D.OverlapCircle(corner, _spawnCheckRadius, _tankLayer) == null)
                    {
                        _player.transform.position = corner;
                        _player.gameObject.SetActive(true);
                        yield break;
                    }

                    yield return null;
                }
            }
        }
    }
}