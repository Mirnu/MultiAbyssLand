using Assets.Scripts.Resources.Data;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.World {
    public interface IWorldInteractor {
        public TileBase GetObjects(Vector2 pos);
        public void Damage(Vector3Int pos, int amount);
        public void Put(Resource resource);
    }
}