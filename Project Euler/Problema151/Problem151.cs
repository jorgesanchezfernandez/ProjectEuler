using System.Collections.Generic;

namespace Project_Euler
{
    using Problema151;
    using System;
    using Utils;

    class Problem151
    {
        public static List<double> probabilidadesTotalPara = new List<double> { 0, 0, 0, 0 };
        public static int numeroTotalDeRamas = 0;

        static void Main(string[] args)
        {
            // Inicia el contador:
            DateTime tiempo1 = DateTime.Now;

            var numHojasPorNivel = 0;

            var sumatorioDeLaprobabilidadDeLaRama = 1.0;

            var probabilidadesAcumuladaDelNodoPara = new List<double> { 1, 1, 1, 1 };
            
            var arbol = new NTree<List<int>>(new List<int> { 2, 3, 4, 5 });
            var numeroSolitariosEncontradosIda = 0;
            var numeroSolitariosEncontradosVuelta = 0;

            var listaMemoriaProbabilidades = new Dictionary<string, List<double>>();
            var memoria = new MemoriaParaP151(listaMemoriaProbabilidades, 0);

            //Build the tree
            CalculoNuevoNivel(ref memoria, sumatorioDeLaprobabilidadDeLaRama, ref probabilidadesAcumuladaDelNodoPara,  arbol, ref numeroSolitariosEncontradosIda, ref numeroSolitariosEncontradosVuelta, ref numHojasPorNivel);

            //Traverse taken the probability
            //Check in the same level if the number it was already calculated
            //Save this whole number of this level in a list.
            var probabilidadTotal = probabilidadesTotalPara[0] + probabilidadesTotalPara[1] + probabilidadesTotalPara[2] + probabilidadesTotalPara[3];

            // Para el contador e imprime el resultado:
            DateTime tiempo2 = DateTime.Now;
            TimeSpan total = new TimeSpan(tiempo2.Ticks - tiempo1.Ticks);
            Console.WriteLine("TIEMPO: " + total.TotalSeconds);
            Console.WriteLine("PROBABILIDAD TOTAL: " + probabilidadTotal);
            Console.WriteLine("TOTAL NUMERO DE RAMAS: " + numeroTotalDeRamas);
            Console.WriteLine("MEDIA: " + probabilidadTotal / numeroTotalDeRamas);

            Console.ReadKey();
        }

        private static void CalculoNuevoNivel(ref MemoriaParaP151 memoria, double sumatorioDeLaprobabilidadDeLaRama, ref List<double> probabilidadesAcumuladaDelNodoPara, NTree<List<int>> arbol, ref int numeroSolitariosEncontradosIda, ref int numeroSolitariosEncontradosVuelta, ref int numHojasPorNivel)
        {
            //Visualizador(memoria, arbol, numeroTotalDeRamas);
            // Cálculo de probabilidades por nivel y sumatorio de la rama
            var numeroElementosDelNodo = Convert.ToDouble(arbol.GetNode().Count);
            var probabilidadDelNivel = 1 / numeroElementosDelNodo;
            sumatorioDeLaprobabilidadDeLaRama = sumatorioDeLaprobabilidadDeLaRama * probabilidadDelNivel;

            if (!EsHojaTrueYTrataNumeroSolitario(sumatorioDeLaprobabilidadDeLaRama, arbol, numeroSolitariosEncontradosIda, ref numHojasPorNivel))
            { 
                numHojasPorNivel = 0;
                // Recorremos cada elemento del nodo
                for (var i = arbol.GetNode().Count - 1; i >= 0; i--)
                {
                    NTree<List<int>> nuevoArbol = ObtenerNuevoNodo(arbol, i);

                    if (NoHemosCalculadoElNuevoNodoPreviamente(memoria, arbol, i, nuevoArbol))
                    {
                        CalculoNuevoNivel(ref memoria, sumatorioDeLaprobabilidadDeLaRama, ref probabilidadesAcumuladaDelNodoPara, nuevoArbol, ref numeroSolitariosEncontradosIda,ref numeroSolitariosEncontradosVuelta, ref numHojasPorNivel);
                        //Sumamos un solitario encontrado

                        if ((nuevoArbol.GetNode().Count == 1) && (nuevoArbol.GetNode()[0] != 5))
                        {
                            numeroSolitariosEncontradosVuelta++;
                        }
                        var numeroElementosDelNuevoNodo = Convert.ToDouble(nuevoArbol.GetNode().Count);
                        probabilidadesAcumuladaDelNodoPara[numeroSolitariosEncontradosVuelta] = probabilidadesAcumuladaDelNodoPara[numeroSolitariosEncontradosVuelta] * (1 / numeroElementosDelNuevoNodo);

                        memoria.listaMemoriaProbabilidades.Add(ConvertirListaEnCadena(nuevoArbol.GetNode()), probabilidadesAcumuladaDelNodoPara);
                        memoria.ramasTotales = numHojasPorNivel;
                    }
                    else
                    {
                        var probabilidadGuardadaDeNodoRepetido = new List<double>();

                        if (memoria.listaMemoriaProbabilidades.TryGetValue(ConvertirListaEnCadena(nuevoArbol.GetNode()), out probabilidadGuardadaDeNodoRepetido))
                        {
                            for (var z = 0; z <= 3; z++)
                            {
                                probabilidadesTotalPara[z] += probabilidadGuardadaDeNodoRepetido[z];

                            }
                            numHojasPorNivel += memoria.ramasTotales;
                        }
                    }
                }
                numeroTotalDeRamas += numHojasPorNivel;
            }
        }

        private static NTree<List<int>> ObtenerNuevoNodo(NTree<List<int>> arbol, int i)
        {
            // Creamos el nuevo nodo por cada uno de los numeros
            List<int> nuevoNodo = CrearNuevoNodo(arbol, i);
            var nuevoArbol = new NTree<List<int>>(nuevoNodo);
            return nuevoArbol;
        }

        private static bool NoHemosCalculadoElNuevoNodoPreviamente(MemoriaParaP151 memoria, NTree<List<int>> arbol, int i, NTree<List<int>> nuevoArbol)
        {
            return !memoria.listaMemoriaProbabilidades.ContainsKey(ConvertirListaEnCadena(nuevoArbol.GetNode())) && ((i < arbol.GetNode().Count - 1) && (arbol.GetNode()[i] != arbol.GetNode()[i + 1]));
        }

        private static bool EsHojaTrueYTrataNumeroSolitario(double sumatorioDeLaprobabilidadDeLaRama, NTree<List<int>> arbol, int numeroSolitariosEncontradosIda, ref int numeroTotalDeHojasPorNodo)
        {
            if (arbol.GetNode().Count == 1)
            {
                if (arbol.GetNode()[0] == 5)
                {
                    numeroTotalDeHojasPorNodo++;
                    probabilidadesTotalPara[numeroSolitariosEncontradosIda] += sumatorioDeLaprobabilidadDeLaRama;
                    return true;
                }
                    numeroSolitariosEncontradosIda++;
            }
            return false;
        }

        private static void Visualizador(MemoriaParaP151 memoria, NTree<List<int>> arbol, int numeroTotalDeRamas)
        {
                Console.WriteLine("Nodo: ");
                foreach(var nodo in arbol.GetNode())
                {
                    Console.WriteLine(nodo);
                }
                Console.WriteLine("Numero de ramas totales: " + numeroTotalDeRamas);
                //Console.WriteLine("Sumatorio de ramas de vuelta: " + sumatorioDeNumeroDeRamasVuelta);
        }

        private static List<int> CrearNuevoNodo(NTree<List<int>> arbol, int i)
        {
            var nuevoNodo = new List<int>();
            for (var j = 0; j < arbol.GetNode().Count; j++)
            {
                if (j != i)
                {
                    nuevoNodo.Add(arbol.GetNode()[j]);
                }
            }
            if (arbol.GetNode()[i] == 2)
            {
                nuevoNodo.Add(3);
                nuevoNodo.Add(4);
                nuevoNodo.Add(5);
            }
            if (arbol.GetNode()[i] == 3)
            {
                nuevoNodo.Add(4);
                nuevoNodo.Add(5);
            }
            if (arbol.GetNode()[i] == 4)
            {
                nuevoNodo.Add(5);
            }
            nuevoNodo.Sort(); // Importante para no repetir las busquedas
            return nuevoNodo;
        }

        public static string ConvertirListaEnCadena (List<int> nodo)
        {
            var cadena = "";
            for(var i=0; i<nodo.Count; i++)
            {
                if (i==0)
                {
                    cadena += nodo[i].ToString();
                }
                else
                {
                  cadena += "," + nodo[i].ToString();
                }
            }
            return cadena;
        }
    }
}
