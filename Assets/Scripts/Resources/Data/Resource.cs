using Assets.Scripts.Resources.Tools;
using Mirror;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace Assets.Scripts.Resources.Data
{
    [CreateAssetMenu(menuName = "Resources/New Resource")]
    public class Resource : ScriptableObject
    {
        [SerializeField] private bool _isTakenInHand = true;
        public bool IsTakenInHand => _isTakenInHand;

        public Sprite SpriteInInventary;
        public string Name;
        public Tool Tool;
        [TextArea(3, 10)] public string Info;
    }
}
