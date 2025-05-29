using Assets._Project.Scripts.Gameplay.TanksLogic;
using Assets._Project.Scripts.Gameplay.TanksLogic.Control;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.EnemyLogic
{
    public class EnemyBehaviour : MonoBehaviour
    {
        [SerializeField] private DefaultTankMovement _movement;

        private IControlHandler _controllHandler;

        private EnemyAIControl _aiControl;

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            _movement.Init();
            _aiControl = new EnemyAIControl();
            _controllHandler = new EnemyTankControlHandler(_aiControl, _movement);
        }

        private void FixedUpdate()
        {
            _controllHandler.Handle();
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            _aiControl.ForceTurnInPlace();
        }
    }
}