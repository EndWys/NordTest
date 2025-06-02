using Assets._Project.Scripts.Gameplay.TanksLogic.Movement;

namespace Assets._Project.Scripts.Gameplay.TanksLogic.Control
{
    public struct DefaultMovementControlData : IControllData, IMoveData
    {
        public float Move;
        public float Rotation;
    }
    public struct SeparatedTrackMovementControlData : IControllData, IMoveData
    {
        public float LeftTrack;
        public float RightTrack;
    }

    public struct GunControlData : IControllData
    {
        public bool Shoot;
    }
}