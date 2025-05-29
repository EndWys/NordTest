using Assets._Project.Scripts.Gameplay.GameInput;
using Assets._Project.Scripts.Gameplay.TanksLogic;
using Assets._Project.Scripts.Gameplay.TanksLogic.Control;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.PlayerLogic
{
    public class PlayerBehaviour : MonoBehaviour
    {
        [SerializeField] private DefaultTankMovement _movement;

        private IControlDataGetter<MoveOnlyTankControlData> _controlDataGetter;

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            _movement.Init();

            _controlDataGetter = new PlayerGameInput();
        }

        private void FixedUpdate()
        {
            HandleControl();
        }

        private void HandleControl()
        {
            var controlData = _controlDataGetter.GetControlData();

            _movement.Move(controlData.MoveData);
        }
    }
}