using Assets.Scripts.Entity;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerManager : EntityManager
    {
        public PlayerInput PlayerInput { get; private set; }

        private void Awake()
        {
            PlayerInput = new();
            PlayerInput.Enable();

            Application.targetFrameRate = 60;
        }
    }
}