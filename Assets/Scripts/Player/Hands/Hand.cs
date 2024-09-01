using Assets.Scripts.Resources.Data;
using Assets.Scripts.Resources.Tools;
using Mirror;
using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Player.Hands
{
    public class Hand : MonoBehaviour
    {
        private Resource _currentResource;
        private Tool _currentTool;
        [SerializeField] private Resource _baseResource;

        public Resource CurrentResource => _currentResource;
        public Transform Transform => transform;
        public bool IsEmpty => _currentResource == _baseResource;
        public event Action<Resource> ToolChanged; 

        public void Equip(Resource resource)
        {
            if (_currentResource == resource) return;
            if (_currentTool != null) 
                Object.Destroy(_currentTool.gameObject);

            _currentResource = resource;
            ToolChanged?.Invoke(resource);
            _currentTool = createTool(resource);
        }

        public void EmptyHand() => Equip(_baseResource);

        private Tool createTool(Resource resource)
        {
            Tool tool = Object.Instantiate(resource.Tool, transform.position, transform.rotation);
            return tool;
        }
    }
}
