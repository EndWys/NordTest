using Assets._Project.Scripts.Gameplay.EnemyLogic;
using Assets._Project.Scripts.Gameplay.PlayerLogic;
using UnityEngine;

namespace Assets._Project.Scripts.EntryPoint
{
    public class GameSceneEntryPoint : MonoBehaviour
    {
        [SerializeField] private PlayerSpawner _playerSpawner;
        [SerializeField] private EnemySpawner _enemySpawner;

        private void Start()
        {
            _playerSpawner.Init();
            _enemySpawner.Init();
        }
    }
}