using Assets.Scripts.Misc;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player.Components.Controllers
{
    public enum ArmAction
    {
        Up = 20,
        Right = 21,
        Down = 22,
        Left = 23
    }

    public class ArmAnimationController : PlayerComponent
    {
        [SerializeField] private ToolContainer _hand;
        [SerializeField] private PlayerDirectionController _directionController;
        [SerializeField] private List<GameObject> _handPoints;
        [SerializeField] private ArmAnimator _armAnimator;

        public override void ClientTick()
        {
            int hours = AngleUtils.GetHours();
            if (_hand.IsEmpty || !_hand.CurrentResource.IsTakenInHand)
            {
                _hand.transform.localPosition = Vector3.zero;
                return;
            }

            _armAnimator.Play(hours + 8);
            GameObject handPoint = _handPoints[hours];
            _hand.transform.localPosition = handPoint.transform.localPosition;
        }
    }
}