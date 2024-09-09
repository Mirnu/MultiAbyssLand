using Assets.Scripts.Entity;
using Assets.Scripts.Game;
using Assets.Scripts.Player.Components.Controllers;
using UnityEngine;

public class PlayerFacade : EntityFacade
{
    [SerializeField] private ArmAnimator _armAnimator;

    private void Awake()
    {
        FacadeLocator.Singleton.RegisterFacade(this);
    }

    public ArmAnimator ArmAnimator
    {
        get
        {
            return _armAnimator;
        }
    }
}
