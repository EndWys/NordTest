namespace Assets._Project.Scripts.Gameplay.TanksLogic.Control
{
    public interface IControllData { }
    public interface IControlDataGetter<TControlData> where TControlData : IControllData
    {
        TControlData GetControlData();
    }
}