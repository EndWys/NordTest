using Assets._Project.Scripts.Gameplay.TanksLogic.Control;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.GameInput
{
    public class PlayerGunInput : IControlHandler<GunControlData>
    {
        private GunControlData _currentControlData = new GunControlData();

        public void UpdateControlData()
        {
            _currentControlData.Shoot = Input.GetMouseButtonDown(0);
        }

        public GunControlData GetControlData()
        {
            return _currentControlData;
        }
    }
}