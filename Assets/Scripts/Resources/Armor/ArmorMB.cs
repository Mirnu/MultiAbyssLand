using Assets.Scripts.Player;
using Assets.Scripts.Resources.Data;
using UnityEngine;

namespace Assets.Scripts.Resources.Armors
{
    public class ArmorMB : MonoBehaviour
    {
        protected ArmorResource resource;
        
        public void Init(ArmorResource resource)
        {
            this.resource = resource;
        }
    }
}