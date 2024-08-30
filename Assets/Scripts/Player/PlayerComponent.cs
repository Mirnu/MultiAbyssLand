using Assets.Scripts.Entity;
using Assets.Scripts.ILifeCycle;

namespace Assets.Scripts.Player
{
    public class PlayerComponent : EntityComponent, IServerInitializable, IClientTickable, IServerTickable,
        IClientInitializable
    {
        protected PlayerManager playerManager => (PlayerManager)entityManager;

        public virtual void ClientInitialize() { }

        public virtual void ClientTick() { }

        public virtual void ServerInitialize() { }

        public virtual void ServerTick() { }
    }
}
