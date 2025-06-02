using System;
using UnityEngine;

namespace Assets._Project.Scripts.SaveSystem
{
    public interface ITankSave
    {
        TankSaveData GetSaveData();
    }

    [Serializable]
    public class TankSaveData : ISaveData
    {
        public Vector3 Position;
        public Quaternion Rotation;

        public TankSaveData(Vector3 position, Quaternion rotation)
        {
            Position = position;
            Rotation = rotation;
        }
    }
}