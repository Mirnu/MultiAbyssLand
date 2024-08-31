using Assets.Scripts.Misc;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player.Components.Controllers
{
    public class ArmAnimationController : PlayerComponent
    {
        [SerializeField] private ToolContainer _hand;
        [SerializeField] private PlayerDirectionController _directionController;
        [SerializeField] private List<GameObject> _handPoints;
        [SerializeField] private Animator _model;

        public override void ClientTick()
        {
            int hours = AngleUtils.GetHours();
            if (!_hand.IsEmpty) return;

            _model.SetInteger("State", hours + 8);
            GameObject handPoint = _handPoints[hours];
            _hand.transform.localPosition = handPoint.transform.localPosition;
        }
    }
}