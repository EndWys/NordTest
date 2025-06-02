using Assets._Project.Scripts.Gameplay.TanksLogic.Control;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.GameInput
{
    public class PlayerMoveInput : IControlHandler<DefaultMovementControlData>
    {
        private DefaultMovementControlData _currentControlData = new DefaultMovementControlData();

        public void UpdateControlData()
        {
            float move = 0f;

            if (Input.GetKey(KeyCode.W)) move = 1f;
            else if (Input.GetKey(KeyCode.S)) move = -1f;

            float rotate = 0f;
            if (Input.GetKey(KeyCode.A)) rotate = -1f;
            else if (Input.GetKey(KeyCode.D)) rotate = 1f;

            _currentControlData.Move = move;
            _currentControlData.Rotation = rotate;
        }

        public DefaultMovementControlData GetControlData()
        {
            return _currentControlData;
        }
    }
}