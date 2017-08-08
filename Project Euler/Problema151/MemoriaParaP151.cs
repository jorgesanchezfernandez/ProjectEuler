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

        public Dictionary<string, int>numeroDeSoliarios { get; set; }

        public MemoriaParaP151()
        {
            this.listaMemoriaProbabilidades = new Dictionary<string, List<double>>();
            this.hojasTotales = new Dictionary<string, int>();
            this.numeroDeSoliarios = new Dictionary<string, int>();
        }

        public MemoriaParaP151(Dictionary<string, List<double>> listaMemoriaProbabilidades, Dictionary<string, int> ramasTotales, Dictionary<string, int> numeroDeSolitarios)
        {
            this.listaMemoriaProbabilidades = listaMemoriaProbabilidades;
            this.hojasTotales = ramasTotales;
            this.numeroDeSoliarios = numeroDeSoliarios;
        }
    }
}
