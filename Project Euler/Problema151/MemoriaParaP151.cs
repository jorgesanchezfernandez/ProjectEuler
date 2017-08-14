using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Euler.Problema151
{
    class MemoriaParaP151
    {
        public List<int> nodo { get; }

        public List<int> numBranchesPerQuantityOfSinglesSheets { get; }

        public int numTotalBranches { get; }

        public MemoriaParaP151()
        {
            this.nodo = new List<int>();
            this.numBranchesPerQuantityOfSinglesSheets = new List<int> { 0, 0, 0, 0 };
            this.numTotalBranches = 0;
        }

        public MemoriaParaP151(List<int> nodo, List<int> numBranchesPerQuantityOfSinglesSheets)
        {
            this.nodo = nodo;
            this.numBranchesPerQuantityOfSinglesSheets = numBranchesPerQuantityOfSinglesSheets;
            this.numTotalBranches = numBranchesPerQuantityOfSinglesSheets[0] + numBranchesPerQuantityOfSinglesSheets[1] + numBranchesPerQuantityOfSinglesSheets[2] + numBranchesPerQuantityOfSinglesSheets [3];
        }
    }
}
