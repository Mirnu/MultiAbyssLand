using Assets.Scripts.Entity;
using Assets.Scripts.ILifeCycle;

namespace Assets.Scripts.Player
{
    public class PlayerComponent : EntityComponent
    {
        protected PlayerManager playerManager => (PlayerManager)entityManager;
    }
}
