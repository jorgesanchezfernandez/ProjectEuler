using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Euler.Problema151
{
    class HashMapPlus
    {
        public List<MemoriaParaP151>[,] HashMapP;

        private MemoriaParaP151 nodeAux;

        public HashMapPlus ()
        {
            HashMapP = new List<MemoriaParaP151>[9,6];
        }

        public void Add (MemoriaParaP151 memory)
        {
            if (HashMapP[memory.nodo.Count, memory.nodo[0]] == null)
                HashMapP[memory.nodo.Count, memory.nodo[0]] = new List<MemoriaParaP151>();

            HashMapP[memory.nodo.Count, memory.nodo[0]].Add(memory);
        }

        public MemoriaParaP151 Find (List<int> node)
        {
            try
            {
                return HashMapP[node.Count, node[0]].Find(innerNode => innerNode.nodo.SequenceEqual(node)); ;
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }
    }
}
