using Assets._Project.Scripts.Gameplay.EnemyLogic;
using Assets._Project.Scripts.Gameplay.GameInput;
using Assets._Project.Scripts.Gameplay.TanksLogic;
using Assets._Project.Scripts.Gameplay.TanksLogic.Control;
using Assets._Project.Scripts.Gameplay.TanksLogic.Shooting;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.PlayerLogic
{
    public class PlayerBehaviour : MonoBehaviour
    {
        [SerializeField] private DefaultTankMovement _movement;
        [SerializeField] private TankGun _gun;

        private IControlDataGetter<ShootingTankControlData> _controlDataGetter;

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            _movement.Init();
            _gun.Init();

            _controlDataGetter = new PlayerGameInput();
        }

        private void Update()
        {
            _controlDataGetter.UpdateControlData();

            var controlData = _controlDataGetter.GetControlData();

            _gun.TryToShoot(controlData.ShootData);
        }

        private void FixedUpdate()
        {
            var controlData = _controlDataGetter.GetControlData();

            _movement.Move(controlData.MoveData);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out EnemyBehaviour bullet))
            {
                gameObject.SetActive(false);
                return;
            }
        }
    }
}