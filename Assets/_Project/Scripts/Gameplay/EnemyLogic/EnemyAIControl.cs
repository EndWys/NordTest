using Assets._Project.Scripts.Gameplay.TanksLogic.Control;
using UnityEngine;

public class EnemyAIControl : IControlDataGetter<MoveOnlyTankControlData>
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

    public MoveOnlyTankControlData GetControlData()
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

        return _currentControlData;
    }

    private void HandleTurnInPlace()
    {
        float rotationStep = _changingDirectionAngel * Time.fixedDeltaTime;
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
        _moveTimer -= Time.fixedDeltaTime;
        if (_moveTimer <= 0f)
        {
            EnterTurnWhileMovingState();
        }

        _currentControlData.MoveData.Move = 1f;
        _currentControlData.MoveData.Rotation = 0f;
    }

    private void HandleTurnWhileMoving()
    {
        float rotationStep = _changingDirectionAngel * Time.fixedDeltaTime;
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
