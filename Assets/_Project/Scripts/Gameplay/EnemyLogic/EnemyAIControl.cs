using Assets._Project.Scripts.Gameplay.TanksLogic.Control;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.EnemyLogic
{
    public interface ITurnInPlace
    {
        void ForceTurnInPlace();
    }

    public class EnemyAIControl : IControlDataGetter<MoveOnlyTankControlData>, ITurnInPlace
    {
        private enum AIState { TurnInPlace, MoveForward, TurnWhileMoving }

        private AIState _currentState;

        private float _changingDirectionAngel = 90f;

        private float _targetAngle;
        private float _rotatedAngle;
        private float _rotationDirection;

        private float _moveTimer;
        private float _moveDuration;

        private MoveOnlyTankControlData _currentControlData = new MoveOnlyTankControlData();

        public EnemyAIControl()
        {
            EnterTurnInPlaceState();
        }

        public void UpdateControlData()
        {
            switch (_currentState)
            {
                case AIState.TurnInPlace:
                    HandleTurnInPlace();
                    break;

                case AIState.MoveForward:
                    HandleMoveForward();
                    break;

                case AIState.TurnWhileMoving:
                    HandleTurnWhileMoving();
                    break;

                default:
                    _currentControlData = new MoveOnlyTankControlData();
                    break;
            }
        }

        public MoveOnlyTankControlData GetControlData()
        {
            return _currentControlData;
        }

        private void HandleTurnInPlace()
        {
            float rotationStep = _changingDirectionAngel * Time.deltaTime;
            _rotatedAngle += rotationStep;

            if (_rotatedAngle >= Mathf.Abs(_targetAngle))
            {
                EnterMoveForwardState();
            }

            _currentControlData.MoveData.Move = 0f;
            _currentControlData.MoveData.Rotation = Mathf.Sign(_targetAngle);
        }

        private void HandleMoveForward()
        {
            _moveTimer -= Time.deltaTime;
            if (_moveTimer <= 0f)
            {
                EnterTurnWhileMovingState();
            }

            _currentControlData.MoveData.Move = 1f;
            _currentControlData.MoveData.Rotation = 0f;
        }

        private void HandleTurnWhileMoving()
        {
            float rotationStep = _changingDirectionAngel * Time.deltaTime;
            _rotatedAngle += rotationStep;
            if (_rotatedAngle >= Mathf.Abs(_targetAngle))
            {
                EnterMoveForwardState();
            }

            _currentControlData.MoveData.Move = 1f;
            _currentControlData.MoveData.Rotation = Mathf.Sign(_targetAngle);
        }

        public void ForceTurnInPlace()
        {
            if (_currentState == AIState.TurnInPlace)
                return;

            EnterTurnInPlaceState();
        }

        private void EnterTurnInPlaceState()
        {
            _currentState = AIState.TurnInPlace;
            _rotatedAngle = 0f;
            _targetAngle = Random.value < 0.5f ? -_changingDirectionAngel : _changingDirectionAngel;
        }

        private void EnterMoveForwardState()
        {
            _currentState = AIState.MoveForward;
            _moveDuration = Random.Range(1.5f, 3.0f);
            _moveTimer = _moveDuration;
        }

        private void EnterTurnWhileMovingState()
        {
            _currentState = AIState.TurnWhileMoving;
            _rotatedAngle = 0f;
            _targetAngle = Random.value < 0.5f ? -_changingDirectionAngel : _changingDirectionAngel;
        }
    }
}