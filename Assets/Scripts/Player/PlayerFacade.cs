using Assets.Scripts.Entity;
using Assets.Scripts.Game;
using Assets.Scripts.Player.Components.Controllers;
using UnityEngine;

public class PlayerFacade : EntityFacade
{
    [SerializeField] private ArmAnimator _armAnimator;
    [SerializeField] private GameObject _character;

    private void Awake()
    {
        FacadeLocator.Singleton.RegisterFacade(this);
    }

    public GameObject Character
    {
        get => _character;
    }

    public ArmAnimator ArmAnimator
    {
        get => _armAnimator;
    }
}
