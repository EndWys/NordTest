using Assets._Project.Scripts.Gameplay.EnemyLogic;
using Assets._Project.Scripts.Gameplay.GameInput;
using Assets._Project.Scripts.Gameplay.TanksLogic;
using Assets._Project.Scripts.Gameplay.TanksLogic.Control;
using Assets._Project.Scripts.Gameplay.TanksLogic.Shooting;
using Assets._Project.Scripts.SaveSystem;
using System;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.PlayerLogic
{
    public class PlayerBehaviour : MonoBehaviour, ITankSave
    {
        [SerializeField] private DefaultTankMovement _movement;
        [SerializeField] private TankGun _gun;

        private IControlHandler<GunControlData> _gunControl;

        /* To change the control principles, we will also need to use IControlHandler<T>, where T will be a new type 
          (which will carry the values for the rotation of each individual track) */

        private IControlHandler<DefaultMovementControlData> _moveControl;

        public event Action OnHit;

        public void Init()
        {
            _movement.Init();
            _gun.Init();

            _gunControl = new PlayerGunInput();
            _moveControl = new PlayerMoveInput();
        }

        private void Update()
        {
            _gunControl.UpdateControlData();
            _moveControl.UpdateControlData();

            _gun.TryToShoot(_gunControl.GetControlData());
        }

        private void FixedUpdate()
        {
            _movement.TryToMove(_moveControl.GetControlData());
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out DefaultEnemyBehaviour bullet))
            {
                OnHit?.Invoke();
                return;
            }
        }

        public TankSaveData GetSaveData()
        {
            return new TankSaveData(transform.position, transform.rotation);
        }
    }
}