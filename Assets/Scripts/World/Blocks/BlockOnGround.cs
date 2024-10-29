using Assets.Scripts.Resources.Crafting;
using Mirror;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BlockOnGround : NetworkBehaviour {
    [SerializeField] private RecipeComponent recipeComponent;
    
    private void Awake() {
        //GetComponent<SpriteRenderer>().sprite = recipeComponent.resource.SpriteInInventary;
    }
 
    public void SetComponent(RecipeComponent component) { 
        recipeComponent = component; 
        //GetComponent<SpriteRenderer>().sprite = recipeComponent.resource.SpriteInInventary; 
    }

    //[Server]
    private void OnTriggerEnter(Collider other) {
        if (!other.gameObject.GetComponentInParent<PlayerFacade>()) { return; }        
        //other.transform.parent.gameObject.GetComponentInChildren<ContainerSelectableSlots>(true).AddToFirst(recipeComponent);
        NetworkServer.Destroy(gameObject);
        //
    }

    // на потом когда шансы прикручиватт
    // [Serializable]
    // public class ResourceDrop {

    // }
}