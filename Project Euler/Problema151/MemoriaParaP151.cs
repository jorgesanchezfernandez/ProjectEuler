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

        public int[] guardaNumTotalDeRamasPorSolitarios = new int[] { 0, 0, 0, 0 };

        public ulong numeroDeHojasDelNodo = 0;

        public MemoriaParaP151()
        {
            this.nodo = new List<int>();
        }

        public MemoriaParaP151(List<int> nodo, int[] guardaNumTotalDeRamasPorSolitarios, ulong numeroDeHojasDelNodo)
        {
            this.nodo = nodo;
            this.guardaNumTotalDeRamasPorSolitarios = guardaNumTotalDeRamasPorSolitarios;
            this.numeroDeHojasDelNodo = numeroDeHojasDelNodo;
        }
    }
}
