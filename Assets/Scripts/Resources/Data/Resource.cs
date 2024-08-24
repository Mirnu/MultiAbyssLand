using UnityEngine;

namespace Assets.Scripts.Resources.Data
{
    [CreateAssetMenu(menuName = "Resources/New Resource")]
    public class Resource : ScriptableObject
    {
        public Sprite SpriteInInventary;
        public string Name;
       // public Tool Tool;
        [TextArea(3, 10)] public string Info;
    }
}
