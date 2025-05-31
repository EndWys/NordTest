using Assets._Project.Scripts.Gameplay.TanksLogic.Control;
using Assets._Project.Scripts.Gameplay.TanksLogic.Movement;
using Assets._Project.Scripts.Gameplay.TanksLogic.Shooting;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.GameInput
{
    public class PlayerGameInput : IControlDataGetter<ShootingTankControlData>
    {
        private ShootingTankControlData _currentControlData = new ShootingTankControlData();

        public void UpdateControlData()
        {
            float move = 0f;
            if (Input.GetKey(KeyCode.W)) move = 1f;
            else if (Input.GetKey(KeyCode.S)) move = -1f;

            float rotate = 0f;
            if (Input.GetKey(KeyCode.A)) rotate = -1f;
            else if (Input.GetKey(KeyCode.D)) rotate = 1f;

            _currentControlData.MoveData = new MoveData() { Move = move, Rotation = rotate };


            ShootData shootData = new ShootData();

            if (Input.GetMouseButtonDown(0)) shootData.Shoot = true;

            _currentControlData.ShootData = shootData;
        }

        public ShootingTankControlData GetControlData()
        {
            return _currentControlData;
        }
    }
}