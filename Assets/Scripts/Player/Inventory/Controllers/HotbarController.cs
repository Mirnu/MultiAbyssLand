using Assets.Scripts.Player.Inventory.UI;
using UnityEngine;

namespace Assets.Scripts.Player.Inventory.Controllers
{
    public class HotbarController : MonoBehaviour
    {
        [SerializeField] private SlotView[] _views = new SlotView[9];

        private void Awake()
        {
            
        }
    }
}