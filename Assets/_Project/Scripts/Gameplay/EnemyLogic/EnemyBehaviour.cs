using Assets._Project.Scripts.Gameplay.TanksLogic;
using Assets._Project.Scripts.Gameplay.TanksLogic.Control;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.EnemyLogic
{
    public class EnemyBehaviour : MonoBehaviour
    {
        [SerializeField] private DefaultTankMovement _movement;

        private IControlDataGetter<MoveOnlyTankControlData> _controlDataGetter;

        private ITurnInPlace _turn;

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            _movement.Init();

            var aiController = new EnemyAIControl();

            _turn = aiController;
            _controlDataGetter = aiController;
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

        private void OnCollisionEnter2D(Collision2D collision)
        {
            _turn.ForceTurnInPlace();
        }
    }
}