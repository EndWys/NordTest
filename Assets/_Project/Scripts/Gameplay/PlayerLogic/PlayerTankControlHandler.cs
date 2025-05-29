using Assets._Project.Scripts.Gameplay.TanksLogic;
using Assets._Project.Scripts.Gameplay.TanksLogic.Control;
using Assets._Project.Scripts.Gameplay.TanksLogic.Movement;

public class PlayerTankControlHandler : TankControlHandler<MoveOnlyTankControlData>
{
    private DefaultTankMovement _movement;

    public PlayerTankControlHandler(IControlDataGetter<MoveOnlyTankControlData> dataGetter,
        DefaultTankMovement movement) 
        : base(dataGetter)
    {
        _movement = movement;
    }
    
    public override void Handle()
    {
        MoveOnlyTankControlData controllData = _controlDataGetter.GetControlData();

        DefaultMoveData moveData = controllData.MoveData;

        _movement.Move(moveData);
    }
}
