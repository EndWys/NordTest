using Assets._Project.Scripts.Gameplay.TanksLogic.Movement;
using Assets._Project.Scripts.Gameplay.TanksLogic.Shooting;

namespace Assets._Project.Scripts.Gameplay.TanksLogic.Control
{
    public struct MoveOnlyTankControlData : IControllData
    {
        public MoveData MoveData;
    }

    public struct ShootingTankControlData : IControllData
    {
        public MoveData MoveData;
        public ShootData ShootData;
    }
}