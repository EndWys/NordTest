using Assets._Project.Scripts.Gameplay.EnemyLogic;
using Assets._Project.Scripts.Gameplay.PlayerLogic;
using Assets._Project.Scripts.UI;
using UnityEngine;

namespace Assets._Project.Scripts.EntryPoint
{
    public class GameSceneEntryPoint : MonoBehaviour
    {
        [SerializeField] private GameUI _gameUI;

        [SerializeField] private PlayerSpawner _playerSpawner;
        [SerializeField] private EnemySpawner _enemySpawner;

        private void Start()
        {
            _gameUI.Init();

            _playerSpawner.Init();
            _enemySpawner.Init();
        }
    }
}