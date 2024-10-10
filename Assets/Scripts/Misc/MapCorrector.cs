using Assets.Scripts.World.Blocks;
using Assets.Scripts.World.Managers;
using Mirror;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.Misc
{
    public class MapCorrector : NetworkBehaviour
    {
        [SerializeField] private List<GameObject> _parents;
        [SerializeField] private List<GameObject> trees;
        [SerializeField] private Block abstractBlock;

        private void Start()
        {
            foreach (var parent in _parents)
            {
                foreach (Transform child in parent.transform)
                {
                    float y = child.position.y;
                    float z = (y + 62) / (40 + 62) * 10;
                    child.position = new Vector3(child.position.x, y, z);
                }
            }
            trees.ForEach(x => FirstTypeManager.Singleton.AddToBlocks(x.GetComponent<Block>(), x.transform.position, x.GetComponent<SpriteRenderer>().sprite));
            FirstTypeManager.Singleton.Init();
        }
    }
}