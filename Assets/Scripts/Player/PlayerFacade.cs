using Assets.Scripts.Entity;
using Assets.Scripts.Game;
using Assets.Scripts.Player.Components.Controllers;
using UnityEngine;

public class PlayerFacade : EntityFacade
{
    [SerializeField] private ArmAnimationController _armAnimationController;

    private void Awake()
    {
        FacadeLocator.Singleton.RegisterFacade(this);
    }

    public void PlayArmAnimation(ArmAction action)
    {
        _armAnimationController.PlayActionAnimation(action);
    }
}
