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

        public int[] guardaNumeroDeRamasPorCantidadDeSolitariosEnNodo = new int[] { 0, 0, 0, 0 };

        public ulong numeroDeHojasDelNodo = 0;

        public MemoriaParaP151()
        {
            this.nodo = new List<int>();
        }

        public MemoriaParaP151(List<int> nodo, int[] guardaNumeroDeRamasPorCantidadDeSolitariosEnNodo, ulong numeroDeHojasDelNodo)
        {
            this.nodo = nodo;
            this.guardaNumeroDeRamasPorCantidadDeSolitariosEnNodo = guardaNumeroDeRamasPorCantidadDeSolitariosEnNodo;
            this.numeroDeHojasDelNodo = numeroDeHojasDelNodo;
        }

        public void AddSolitariosANodo(int[] guardaNumeroDeRamasPorCantidadDeSolitariosEnNodo)
        {
            for(var i=0; i <= guardaNumeroDeRamasPorCantidadDeSolitariosEnNodo.Length - 1; i++ )
            {
                this.guardaNumeroDeRamasPorCantidadDeSolitariosEnNodo[i] += guardaNumeroDeRamasPorCantidadDeSolitariosEnNodo[i];
            }
        }
    }
}
