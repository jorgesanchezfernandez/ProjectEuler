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

        public double numeroDeSolitariosHastaHoja;

        public double numeroTotalDeHojas;

        public MemoriaParaP151()
        {
            this.nodo = new List<int>();
            this.numeroDeSolitariosHastaHoja = 0.0;
            this.numeroTotalDeHojas = 0.0;
        }

        public MemoriaParaP151(List<int> nodo, double numeroDeSolitariosHastaHoja, double numeroDeHojas)
        {
            this.nodo = nodo;
            this.numeroDeSolitariosHastaHoja = numeroDeSolitariosHastaHoja;
            this.numeroTotalDeHojas = numeroDeHojas;
        }

        public void AddSolitariosANodo(double numeroDeSolitariosHastaHoja)
        {
            this.numeroDeSolitariosHastaHoja += numeroDeSolitariosHastaHoja;
        }
        public void AddNumeroDeHojas(double numeroTotalDeHojas)
        {
            this.numeroTotalDeHojas += numeroTotalDeHojas;
        }
    }
}
