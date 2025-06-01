using System.Collections;

namespace Assets._Project.Scripts.SaveSystem
{
    public interface ISaveData { }

    public interface ISaveLoad<TData> where TData : ISaveData 
    {
        void Save();

        void Load(TData data);
    }
}