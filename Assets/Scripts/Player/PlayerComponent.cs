using Assets.Scripts.Entity;

namespace Assets.Scripts.Player
{
    public class PlayerComponent : EntityComponent
    {
        protected PlayerManager GetManager()
        {
            return (PlayerManager)entityManager;
        }
    }
}
