using Assets._Project.Scripts.Gameplay.TanksLogic;
using Assets._Project.Scripts.Gameplay.TanksLogic.Control;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.EnemyLogic
{
    public class EnemyBehaviour : MonoBehaviour
    {
        [SerializeField] private DefaultTankMovement _movement;

        private EnemyAIControl _aiControl;

        private IControlDataGetter<MoveOnlyTankControlData> _controlDataGetter;

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            _movement.Init();
            _aiControl = new EnemyAIControl();
            _controlDataGetter = _aiControl;
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