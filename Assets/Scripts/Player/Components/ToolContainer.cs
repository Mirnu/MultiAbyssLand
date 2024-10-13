using Assets.Scripts.Resources.Data;
using Assets.Scripts.Resources.Tools;
using Mirror;
using System;
using UnityEngine;

namespace Assets.Scripts.Player.Components
{
    public class ToolContainer : PlayerComponent
    {
        [SerializeField] private Resource _baseResource;
        private Resource _currentResource;
        private Tool _currentTool;
        

        public Resource CurrentResource => _currentResource;
        public bool IsEmpty => _currentResource == _baseResource;
        public event Action<Resource> ToolChanged;

        [Server]
        private void Start() => EmptyHand();

        [TargetRpc]
        private void RpcOnToolChanged()
        {
            ToolChanged?.Invoke(_currentResource);
        }

        [Server]
        public void Equip(Resource resource)
        {
            if (_currentResource == resource) return;
            if (_currentTool != null)
                NetworkServer.Destroy(_currentTool.gameObject);

            _currentResource = resource;
            ToolChanged?.Invoke(_currentResource);
            RpcOnToolChanged();
            _currentTool = createTool(_currentResource);
        }

        [Server]
        public void EmptyHand() => Equip(_baseResource);

        [Server]
        private Tool createTool(Resource resource)
        {
            GameObject instance = Instantiate(resource.Tool.gameObject, gameObject.transform);
            Tool tool = instance.GetComponent<Tool>();
            tool.Init(resource);
            NetworkServer.Spawn(instance, playerManager.gameObject);
            return tool;
        }
    }
}
