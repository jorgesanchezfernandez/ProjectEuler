using Project_Euler.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Euler.Problema151
{
    class HashMapPlus
    {
        private List<MemoriaParaP151> ListMemories;

        public HashMapPlus ()
        {
            ListMemories = new List<MemoriaParaP151>();
        }

        public void Add (MemoriaParaP151 memory)
        {
            ListMemories.Add(memory);
        }

        public MemoriaParaP151 Find(List<int> node)
        {
            return ListMemories.Find(n => n.nodo.SequenceEqual(node));
        }
    }
}
