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
        public Dictionary<string, int> hojasTotales { get; set; }

        public Dictionary<string, List<int>> numeroDeSolitarios { get; set; }

        public MemoriaParaP151()
        {
            listaMemoriaProbabilidades = new Dictionary<string, List<double>>();
            hojasTotales = new Dictionary<string, int>();
            numeroDeSolitarios = new Dictionary<string, List<int>>();
        }

        public MemoriaParaP151(Dictionary<string, List<double>> listaMemoriaProbabilidades, Dictionary<string, int> hojasTotales, Dictionary<string, List<int>> numeroDeSolitarios)
        {
            this.listaMemoriaProbabilidades = listaMemoriaProbabilidades;
            this.hojasTotales = hojasTotales;
            this.numeroDeSolitarios = numeroDeSolitarios;
        }
    }
}
