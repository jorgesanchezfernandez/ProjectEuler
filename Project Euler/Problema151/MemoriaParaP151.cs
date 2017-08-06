using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Euler.Problema151
{
    class MemoriaParaP151
    {
        public Dictionary<string, List<double>> listaMemoriaProbabilidades { get; set; }
        public int ramasTotales { get; set; }

        public MemoriaParaP151(Dictionary<string, List<double>> listaMemoriaProbabilidades, int ramasTotales)
        {
            this.listaMemoriaProbabilidades = listaMemoriaProbabilidades;
            this.ramasTotales = ramasTotales;
        }
    }
}
