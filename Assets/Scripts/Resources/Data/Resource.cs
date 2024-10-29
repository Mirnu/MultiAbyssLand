using Assets.Scripts.Resources.Tools;
using Mirror;
using UnityEngine;

namespace Assets.Scripts.Resources.Data
{
    [CreateAssetMenu(menuName = "Resources/New Resource")]
    public class Resource : ScriptableObject
    {
        [SerializeField] private bool _isTakenInHand = true;
        public bool IsTakenInHand => _isTakenInHand;
        public Tool Tool;
        [TextArea(3, 10)] public string Info;

        [SerializeField] private string _name;
        [SerializeField] private Sprite _spriteInInventory;
        [SerializeField] private int _maxItemsInStack;

        public string Name => _name;
        public Sprite SpriteInInventory => _spriteInInventory;
        public int MaxItemsInStack => _maxItemsInStack;
    }
}
