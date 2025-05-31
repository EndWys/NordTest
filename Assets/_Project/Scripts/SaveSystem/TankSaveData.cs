using System;
using UnityEngine;

namespace Assets._Project.Scripts.SaveSystem
{
    [Serializable]
    public class TankSaveData
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