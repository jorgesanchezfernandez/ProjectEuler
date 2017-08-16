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

        public List<List<double>> probabilityNodeToleafPerSinglesSheets { get; set; }


        public MemoriaParaP151()
        {
            this.nodo = new List<int>();
            this.probabilityNodeToleafPerSinglesSheets = new List<List<double>>();
        }

        public MemoriaParaP151(List<int> nodo, List<List<double>> probabilityNodeToleafPerSinglesSheets)
        {
            this.nodo = nodo;
            this.probabilityNodeToleafPerSinglesSheets = probabilityNodeToleafPerSinglesSheets;
        }
    }
}
