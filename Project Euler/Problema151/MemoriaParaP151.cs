using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Euler.Problema151
{
    class MemoriaParaP151
    {
        List<double> probabilidadesDelNodo;

        int hojasDelNodo;

        public MemoriaParaP151()
        {
            this.probabilidadesDelNodo = new List<double>();
            this.hojasDelNodo = 0;
        }

        public MemoriaParaP151(List<double> probabilidadesDelNodo, int hojasDelNodo)
        {
            this.probabilidadesDelNodo = probabilidadesDelNodo;
            this.hojasDelNodo = hojasDelNodo;
        }
    }
}
