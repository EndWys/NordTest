using Assets._Project.Scripts.Extansions;
using Assets._Project.Scripts.SaveSystem;
using Assets._Project.Scripts.UI;
using System.Collections;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.PlayerLogic
{
    public class PlayerSpawner : TankSpawner<TankSaveData>
    {
        [SerializeField] private PlayerBehaviour _player;

        [Header("Spawn Zone Settings")]
        [SerializeField] private Collider2D _spawnZone;
        [SerializeField] private LayerMask _tankLayer;
        [SerializeField] private float _spawnCheckRadius = 1f;

        private Vector2[] _cornerPoints;

        protected override string _saveFileName => "player_spawner";

        public override void Init()
        {
            _cornerPoints = _spawnZone.GetCornerPoints();

            _player.Init();
            _player.OnHit += Despawn;

            base.Init();
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
            yield return Spawn();

            GameUI.Instance.HideCenterPanel();
        }

        protected override IEnumerator Spawn()
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

        public override void Load(TankSaveData save)
        {
            _player.transform.position = save.Position;
            _player.transform.rotation = save.Rotation;
        }

        public override void Save()
        {
            _savingService.Save(new TankSaveData(_player.transform.position, _player.transform.rotation));
        }
    }
}