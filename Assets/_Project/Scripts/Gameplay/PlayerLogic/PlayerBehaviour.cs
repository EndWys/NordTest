using Assets._Project.Scripts.Gameplay.GameInput;
using Assets._Project.Scripts.Gameplay.TanksLogic;
using Assets._Project.Scripts.Gameplay.TanksLogic.Control;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.PlayerLogic
{
    public class PlayerBehaviour : MonoBehaviour
    {
        [SerializeField] private DefaultTankMovement _movement;

        private IControlHandler _controllHandler;

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            _movement.Init();

            _controllHandler = new PlayerTankControlHandler(new PlayerGameInput(), _movement);
        }

        private void FixedUpdate()
        {
            _controllHandler.Handle();
        }
    }
}