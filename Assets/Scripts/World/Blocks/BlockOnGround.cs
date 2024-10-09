using Assets.Scripts.Resources.Crafting;
using Mirror;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class BlockOnGround : NetworkBehaviour {
    [SerializeField] private RecipeComponent recipeComponent;
    
    private void Awake() {
        GetComponent<SpriteRenderer>().sprite = recipeComponent.resource.SpriteInInventary;
    }
 
    public void SetComponent(RecipeComponent component) => recipeComponent = component;

    private void OnTriggerEnter2D(Collider2D other) {
        if (!other.gameObject.GetComponentInParent<PlayerFacade>()) { return; }
        Debug.LogWarning("NIGGERS");
    }
}