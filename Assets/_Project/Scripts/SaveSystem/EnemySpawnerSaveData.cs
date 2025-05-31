using System;

namespace Assets._Project.Scripts.SaveSystem
{
    [Serializable]
    public class EnemySpawnerSaveData
    {
        public TankSaveData[] EnemySaveDatas;
        public EnemySpawnerSaveData(TankSaveData[] savedEnemies)
        {
            EnemySaveDatas = savedEnemies;
        }
    }
}