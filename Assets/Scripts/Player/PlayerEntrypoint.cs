using Mirror;
using UnityEngine;

namespace Assets.Scripts.Player {
    public class PlayerEntrypoint : NetworkBehaviour {
        public void OnEntry() {
            Debug.Log("PlayerEntrypoint");
        }
    }
}