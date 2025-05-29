using Assets._Project.Scripts.Gameplay.TanksLogic;
using Assets._Project.Scripts.Gameplay.TanksLogic.Control;

public class EnemyTankControlHandler : TankControlHandler<MoveOnlyTankControlData>
{
    private DefaultTankMovement _movement;

    public EnemyTankControlHandler(IControlDataGetter<MoveOnlyTankControlData> dataGetter,
        DefaultTankMovement movement)
        : base(dataGetter)
    {
        _movement = movement;
    }

    public override void Handle()
    {
        MoveOnlyTankControlData controllData = _controlDataGetter.GetControlData();
        _movement.Move(controllData.MoveData);
    }
}
