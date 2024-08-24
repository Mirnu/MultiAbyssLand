using Mirror;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player.Data
{
    public class PlayerBoost : NetworkBehaviour
    {
        [SyncVar] private float _speedBoost = 1;

        public float SpeedBoost
        {
            get { return _speedBoost; }
            set { _speedBoost = value; }
        }
    }
}