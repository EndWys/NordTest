namespace Assets._Project.Scripts.Gameplay.TanksLogic.Control
{
    public interface IControlHandler
    {
        void Handle();
    }

    public abstract class TankControlHandler<TControllData> : IControlHandler where TControllData : IControllData
    {
        protected IControlDataGetter<TControllData> _controlDataGetter;

        public TankControlHandler(IControlDataGetter<TControllData> dataGetter)
        {
            _controlDataGetter = dataGetter;
        }

        public abstract void Handle();
    }
}