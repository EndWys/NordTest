namespace Assets._Project.Scripts.Gameplay.TanksLogic.Movement
{
    public interface IMoveData { }
    public interface IMove<TData> where TData : IMoveData
    {
        void TryToMove(TData moveData);
    }
}