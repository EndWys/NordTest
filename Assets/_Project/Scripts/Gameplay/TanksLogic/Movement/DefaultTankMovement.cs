using Assets._Project.Scripts.Gameplay.TanksLogic.Control;
using Assets._Project.Scripts.Gameplay.TanksLogic.Movement;
using Assets._Project.Scripts.Gameplay.TanksLogic.Scriptables;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.TanksLogic
{
     /* 
     * In the future, we will be able to create a new class that implements IMove<T>
     * and replace the tank movement method — for example, with separate control for each track, etc.
     */

    [RequireComponent(typeof(Rigidbody2D))]
    public class DefaultTankMovement : MonoBehaviour, IMove<DefaultMovementControlData>
    {
        [SerializeField] private TankMovementSettings _movementSettings;

        private Rigidbody2D _rb;

        public void Init()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void TryToMove(DefaultMovementControlData moveData)
        {
            Vector2 forward = transform.up;
            _rb.MovePosition(_rb.position + forward * moveData.Move * _movementSettings.MoveSpeed * Time.fixedDeltaTime);

            float rotation = -moveData.Rotation * _movementSettings.RotationSpeed * Time.fixedDeltaTime;
            _rb.MoveRotation(_rb.rotation + rotation);
        }
    }
}