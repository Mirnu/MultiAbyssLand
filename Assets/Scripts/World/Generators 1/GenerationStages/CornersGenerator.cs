using Assets.Scripts.World.Internal;
using Mirror;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.World.Generators.GenerationStages
{
    public class CornersGenerator : AbstractGenerator
    {
        private const int COST_GENERATION = 10;
        private const string NAME_GENEARATION = "Corners";

        private int[,] map;
        [SerializeField] private WorldModel model;

        public override int CostGeneration => COST_GENERATION;
        public override string NameGeneration => NAME_GENEARATION;
        public override int Order => 1;

        public override void OnStartServer()
        {
            map = model.Map;
        }

        public override IEnumerator Generate()
        {
            Debug.Log("Corners generation started");
            map[map.GetUpperBound(0) / 2, map.GetUpperBound(0) - 1] = 2;
            map[map.GetUpperBound(0) / 2, map.GetUpperBound(0) / 2] = 2;
            map[0, map.GetUpperBound(1) / 2] = 2;
            map[map.GetUpperBound(0) - 1, map.GetUpperBound(1) / 2] = 2;
            GenerateLine(map.GetUpperBound(0) / 2, map.GetUpperBound(0) / 2, map.GetUpperBound(0) / 2, map.GetUpperBound(1) - 1);
            GenerateLine(map.GetUpperBound(0) / 2, map.GetUpperBound(0) / 2, 0, map.GetUpperBound(1) / 2);
            GenerateLine(map.GetUpperBound(0) / 2, map.GetUpperBound(0) / 2, map.GetUpperBound(0), map.GetUpperBound(1) / 2);
            FillAreaFromCorner(99, 99, 2, 1);
            FillAreaFromCorner(0, 99, 2, 3);
            yield return new WaitForSeconds(0.1f); 
        }

        private void GenerateLine(int x0, int y0, int x1, int y1)
        {
            var l = x0 > x1 ? -1 : 1;
            var l1 = y0 > y1 ? -1 : 1;
            int j = y0;
            int i = x0;
            while (i != x1 || j != y1)
            {
                map[i, j] = 2;
                if (j != y1)
                {
                    j += l1;
                }
                if (i != x1)
                {
                    i += l;
                }
            }
        }

        private void FillAreaFromCorner(int startX, int startY, int borderIndex, int fillIndex)
        {
            var b = startY < map.GetUpperBound(0) - 1 ? 1 : -1;
            var a = startX < map.GetUpperBound(0) - 1 ? 1 : -1;
            for (int i = startX; i < map.GetLength(0); i += a)
            {
                if (map[i, startY] == borderIndex) { break; }
                for (int j = startY; j < map.GetLength(0); j += b)
                {
                    if (map[i, j] == borderIndex) { break; }
                    map[i, j] = fillIndex;
                }
            }
        }
    }
}
