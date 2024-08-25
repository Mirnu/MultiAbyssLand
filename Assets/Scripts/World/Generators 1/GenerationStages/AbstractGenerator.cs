using System;
using System.Collections;
using Mirror;

namespace Assets.Scripts.World.Generators.GenerationStages
{
    [Serializable]
    public class AbstractGenerator : NetworkBehaviour
    {
        public virtual int CostGeneration { get; } // The cost of generating this stage
        public virtual int Order { get; }
        public virtual string NameGeneration {get;}
        public virtual IEnumerator Generate() { yield return null; }
    }
}
