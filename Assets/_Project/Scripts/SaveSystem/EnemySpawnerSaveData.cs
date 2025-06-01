using System;

namespace Assets._Project.Scripts.SaveSystem
{
    [Serializable]
    public class EnemySpawnerSaveData : ISaveData
    {
        public TankSaveData[] EnemySaveDatas;
        public EnemySpawnerSaveData(TankSaveData[] savedEnemies)
        {
            EnemySaveDatas = savedEnemies;
        }
    }
}