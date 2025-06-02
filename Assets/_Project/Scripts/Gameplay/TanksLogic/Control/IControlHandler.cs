namespace Assets._Project.Scripts.Gameplay.TanksLogic.Control
{
    public interface IControllData { }
    public interface IControlHandler<TControlData> where TControlData : IControllData
    {
        void UpdateControlData();
        TControlData GetControlData();
    }
}