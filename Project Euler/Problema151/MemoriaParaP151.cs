using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Euler.Problema151
{
    class MemoriaParaP151
    {
        public List<int> nodo;

        public int numSinglesSheetsToLeaf = 0;

        public int numCutsInNodeToLeaf = 0;

        public MemoriaParaP151()
        {
            this.nodo = new List<int>();
        }

        public MemoriaParaP151(List<int> nodo, int numSinglesSheetsToLeaf, int numCutsInNodeToLeaf)
        {
            this.nodo = nodo;
            this.numSinglesSheetsToLeaf = numSinglesSheetsToLeaf;
            this.numCutsInNodeToLeaf = numCutsInNodeToLeaf;
        }
    }
}
