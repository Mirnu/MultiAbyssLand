using Assets.Scripts.Entity;
using Assets.Scripts.Game;
using Assets.Scripts.Player.Components.Controllers;
using Assets.Scripts.Player.Data;
using System.Collections;
using UnityEngine;

public class PlayerFacade : EntityFacade    
{
    [SerializeField] private ArmAnimator _armAnimator;
    [SerializeField] private GameObject _character;
    [SerializeField] private PlayerStats _stats;

    public static GameObject Instance;

    public override void TakeDamage(int damage)
    {
        Debug.Log($"ZOOMBIE DAMAGE: {damage} ZZZZ");
        _stats.Health -= damage;
    }

    private void Awake()
    {
        Instance = _character;
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

    private IEnumerator Death()
    {
        yield return new WaitForSeconds(5);
        _stats.Health = 0;
    }
}
