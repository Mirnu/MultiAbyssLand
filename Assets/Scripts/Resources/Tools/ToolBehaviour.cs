using Mirror;
using UnityEngine;

namespace Assets.Scripts.Resources.Tools
{
    [RequireComponent(typeof(Tool))]
    public class ToolBehaviour : NetworkBehaviour
    {
        protected Tool tool;

        private void Awake()
        {
            tool = GetComponent<Tool>();
        }
    }
}
