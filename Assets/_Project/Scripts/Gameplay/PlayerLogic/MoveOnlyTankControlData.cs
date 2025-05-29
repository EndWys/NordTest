using Assets._Project.Scripts.Gameplay.TanksLogic.Movement;

namespace Assets._Project.Scripts.Gameplay.TanksLogic.Control
{
    public struct MoveOnlyTankControlData : IControllData
    {
        public DefaultMoveData MoveData;
    }

    public struct ShootingTankControlData : IControllData
    {
        public DefaultMoveData MoveData;
        //public DefaultShootingData ShootData;
    }
}