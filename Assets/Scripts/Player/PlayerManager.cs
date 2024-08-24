using Assets.Scripts.Entity;

namespace Assets.Scripts.Player
{
    public class PlayerManager : EntityManager
    {
        public PlayerInput PlayerInput { get; private set; }

        private void Awake()
        {
            PlayerInput = new();
            PlayerInput.Enable();
        }
    }
}