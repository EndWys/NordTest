using Assets._Project.Scripts.Gameplay.TanksLogic.Movement;
using Assets._Project.Scripts.Gameplay.TanksLogic.Scriptables;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.TanksLogic
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class DefaultTankMovement : MonoBehaviour, IMove<MoveData>
    {
        [SerializeField] private TankMovementSettings _movementSettings;

        private Rigidbody2D _rb;

        public void Init()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void Move(MoveData moveData)
        {
            Vector2 forward = transform.up;
            _rb.MovePosition(_rb.position + forward * moveData.Move * _movementSettings.MoveSpeed * Time.fixedDeltaTime);

            float rotation = -moveData.Rotation * _movementSettings.RotationSpeed * Time.fixedDeltaTime;
            _rb.MoveRotation(_rb.rotation + rotation);
        }
    }
}