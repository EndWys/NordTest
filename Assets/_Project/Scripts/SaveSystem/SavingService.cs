using System.IO;
using UnityEngine;

namespace Assets._Project.Scripts.SaveSystem
{
    public class SavingService
    {
        private const string SAVE_FILE_EXTENSION = ".json";

        private string _saveFileName;

        private string _path => Path.Combine(Application.persistentDataPath, _saveFileName + SAVE_FILE_EXTENSION);

        public SavingService(string saveFileName)
        {
            _saveFileName = saveFileName;
        }

        public void Save<T>(T entitiy) where T : class, ISaveData
        {
            File.WriteAllText(_path, JsonUtility.ToJson(entitiy));
        }

        public T Load<T>() where T : class, ISaveData
        {
            if (!File.Exists(_path))
                return null;

            string value = File.ReadAllText(_path);
            if (string.IsNullOrEmpty(value))
                return null;

            return JsonUtility.FromJson<T>(value);
        }
    }
}