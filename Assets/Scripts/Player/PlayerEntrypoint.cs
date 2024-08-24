using Mirror;

namespace Assets.Scripts.Player {
    public class PlayerEntryPoint : NetworkBehaviour {
        [Server]
        public void OnEntry(NetworkConnection conn) {
            RpcEntryServer();
            RpcEntryClient(conn);
        }

        private void RpcEntryServer() 
        {
            
        }

        [TargetRpc]
        private void RpcEntryClient(NetworkConnection conn)
        {

        }
    }
}