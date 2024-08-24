using Assets.Scripts.Entity;
using Assets.Scripts.Game;

public class PlayerFacade : EntityFacade
{
    private void Awake()
    {
        FacadeLocator.Singleton.RegisterFacade(this);
    }
}
