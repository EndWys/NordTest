using Assets._Project.Scripts.Gameplay.TanksLogic;
using Assets._Project.Scripts.Gameplay.TanksLogic.Bullets;
using Assets._Project.Scripts.Gameplay.TanksLogic.Control;
using Assets._Project.Scripts.ObjectPoolSytem;
using System;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.EnemyLogic
{
    public class EnemyBehaviour : PoolObject
    {
        [SerializeField] private DefaultTankMovement _movement;

        private IControlDataGetter<MoveOnlyTankControlData> _controlDataGetter;

        private ITurnInPlace _turn;

        public event Action<EnemyBehaviour> OnHit;

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
        private void Update()
        {
            _controlDataGetter.UpdateControlData();
        }

        private void FixedUpdate()
        {
            var controlData = _controlDataGetter.GetControlData();

            _movement.Move(controlData.MoveData);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out Bullet bullet))
            {
                OnHit.Invoke(this);
                return;
            }

            _turn.ForceTurnInPlace();
        }

        public override void OnGetFromPool()
        {
            gameObject.SetActive(true);
        }

        public override void OnReleaseToPool()
        {
            gameObject.SetActive(false);
        }
    }
}