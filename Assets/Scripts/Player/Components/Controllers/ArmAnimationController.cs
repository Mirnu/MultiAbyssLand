using Assets.Scripts.Misc;
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
        [SerializeField] private Animator _model;

        public override void ClientTick()
        {
            int hours = AngleUtils.GetHours();
            if (_hand.IsEmpty && !_hand.CurrentResource.IsTakenInHand) return;

            _model.SetInteger("State", hours + 8);
            GameObject handPoint = _handPoints[hours];
            _hand.transform.localPosition = handPoint.transform.localPosition;
        }

        public void PlayActionAnimation(ArmAction action)
        {
            _model.Play(action.ToString(), -1, 0);
        }
    }
}