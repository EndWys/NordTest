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
        /*
         * To add shooting capability to the AI tank,
         * you just need to add the TankGun component and a new IControlHandler<GunControlData> 
         * that will decide when the AI tank should shoot and pass the gun control data — this data will then be processed by the TankGun component.
         */

        [SerializeField] private DefaultTankMovement _movement;

        private IControlHandler<DefaultMovementControlData> _moveControl;

        private ITurnInPlace _turn;

        public event Action<EnemyBehaviour> OnHit;

        public void Init()
        {
            _movement.Init();

            var aiMovementControl = new EnemyMoveControl();

            _turn = aiMovementControl;
            _moveControl = aiMovementControl;
        }
        private void Update()
        {
            _moveControl.UpdateControlData();
        }

        private void FixedUpdate()
        {
            _movement.TryToMove(_moveControl.GetControlData());
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