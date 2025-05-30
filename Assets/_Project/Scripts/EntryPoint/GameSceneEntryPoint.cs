using Assets._Project.Scripts.Gameplay.EnemyLogic;
using UnityEngine;

namespace Assets._Project.Scripts.EntryPoint
{
    public class GameSceneEntryPoint : MonoBehaviour
    {
        [SerializeField] private EnemySpawner _enemySpawner;
        private void Start()
        {
            _enemySpawner.Init();
        }
    }
}