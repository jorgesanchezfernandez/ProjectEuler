using System.Collections.Generic;

namespace Project_Euler
{
    using Problema151;
    using System;
    using Utils;

    class Problem151
    {
        private static int sumatorioDeNumeroDeRamasVuelta;

        static void Main(string[] args)
        {
            // Inicia el contador:
            DateTime tiempo1 = DateTime.Now;

            var probabilidadesTotalPara = new List<double> { 0, 0, 0, 0 };
            var sumatorioDeLaprobabilidadDeLaRama = 1.0;

            var probabilidadesAcumuladaDelNodoPara = new List<double> { 1, 1, 1, 1 };
            var sumatorioDeProbabilidadDelNodoActual = 1.0;

            var numeroTotalDeRamas = 0;
            var arbol = new NTree<List<int>>(new List<int> { 2, 3, 4, 5 });
            var numeroSolitariosEncontradosIda = 0;
            var numeroSolitariosEncontradosVuelta = 0;
            sumatorioDeNumeroDeRamasVuelta = 0;
            var listaMemoriaProbabilidades = new Dictionary<string, List<double>>();
            var memoria = new MemoriaParaP151(listaMemoriaProbabilidades, 0);


            //Build the tree
            CalculoNuevoNivel(memoria, ref probabilidadesAcumuladaDelNodoPara, probabilidadesTotalPara, ref sumatorioDeProbabilidadDelNodoActual, ref sumatorioDeLaprobabilidadDeLaRama, arbol, ref numeroSolitariosEncontradosIda, ref numeroSolitariosEncontradosVuelta, ref numeroTotalDeRamas);

            Visualizador(memoria, arbol, numeroTotalDeRamas, sumatorioDeNumeroDeRamasVuelta);
            //Traverse taken the probability
            //Check in the same level if the number it was already calculated
            //Save this whole number of this level in a list.
            var probabilidadTotal = probabilidadesTotalPara[0] + probabilidadesTotalPara[1] + probabilidadesTotalPara[2] + probabilidadesTotalPara[3];

            // Para el contador e imprime el resultado:
            DateTime tiempo2 = DateTime.Now;
            TimeSpan total = new TimeSpan(tiempo2.Ticks - tiempo1.Ticks);
            Console.WriteLine("TIEMPO: " + total.TotalSeconds.ToString());
            Console.WriteLine("PROBABILIDAD TOTAL: " + probabilidadTotal);
            Console.WriteLine("TOTAL NUMERO DE RAMAS: " + numeroTotalDeRamas);
            Console.WriteLine("MEDIA: " + probabilidadTotal / numeroTotalDeRamas);

            Console.ReadKey();
        }

        private static void CalculoNuevoNivel(MemoriaParaP151 memoria, ref List<double> probabilidadesAcumuladaDelNodoPara, List<double> probabilidadesTotalPara, ref double sumatorioDeProbabilidadDelNodoActual, ref double sumatorioDeLaprobabilidadDeLaRama, NTree<List<int>> arbol, ref int numeroSolitariosEncontradosIda, ref int numeroSolitariosEncontradosVuelta, ref int numeroTotalDeRamas)
        {
           
            // Cálculo de probabilidades por nivel y sumatorio de la rama
            var numeroElementosDelNodo = Convert.ToDouble(arbol.GetNode().Count);
            var probabilidadDelNivel = 1 / numeroElementosDelNodo;
            sumatorioDeLaprobabilidadDeLaRama = sumatorioDeLaprobabilidadDeLaRama * probabilidadDelNivel;

            // Recorremos cada elemento del nodo
            for (var i = arbol.GetNode().Count - 1; i >= 0; i--)
            {
                if (arbol.GetNode().Count == 1)
                {
                    if (arbol.GetNode()[i] == 5)
                    {
                        numeroSolitariosEncontradosVuelta = 0;
                        numeroTotalDeRamas++;
                        sumatorioDeNumeroDeRamasVuelta++;
                        probabilidadesTotalPara[numeroSolitariosEncontradosIda] += sumatorioDeLaprobabilidadDeLaRama;
                    }
                    else
                    {
                        numeroSolitariosEncontradosIda++;
                    }
                }

                if ((arbol.GetNode()[i] != 5) || ((arbol.GetNode()[i] == 5) && (arbol.GetNode().Count > 1)))
                {
                    // Creamos el nuevo nodo por cada uno de los numeros
                    List<int> nuevoNodo = CrearNuevoNodo(arbol, i);

                    var nuevoArbol = new NTree<List<int>>(nuevoNodo);

                    if (((i == arbol.GetNode().Count - 1) || ((i < arbol.GetNode().Count - 1) && (arbol.GetNode()[i] != arbol.GetNode()[i + 1]))) && !memoria.listaMemoriaProbabilidades.ContainsKey(ConvertirListaEnCadena(nuevoArbol.GetNode())))
                    {
                        CalculoNuevoNivel(memoria, ref probabilidadesAcumuladaDelNodoPara, probabilidadesTotalPara, ref sumatorioDeProbabilidadDelNodoActual, ref sumatorioDeLaprobabilidadDeLaRama, nuevoArbol, ref numeroSolitariosEncontradosIda, ref numeroSolitariosEncontradosVuelta, ref numeroTotalDeRamas);

                        // Sumamos un solitario encontrado
                        if ((nuevoArbol.GetNode().Count == 1) && (nuevoArbol.GetNode()[0] != 5))
                        {
                            numeroSolitariosEncontradosIda--;
                            numeroSolitariosEncontradosVuelta++;
                        }
                        var numeroElementosDelNuevoNodo = Convert.ToDouble(nuevoArbol.GetNode().Count);
                        probabilidadesAcumuladaDelNodoPara[numeroSolitariosEncontradosVuelta] = probabilidadesAcumuladaDelNodoPara[numeroSolitariosEncontradosVuelta] * (1 / numeroElementosDelNuevoNodo);

                        memoria.listaMemoriaProbabilidades.Add(ConvertirListaEnCadena(nuevoArbol.GetNode()), probabilidadesAcumuladaDelNodoPara);
                        memoria.ramasTotales = sumatorioDeNumeroDeRamasVuelta;
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

                            numeroTotalDeRamas += memoria.ramasTotales;
                            sumatorioDeNumeroDeRamasVuelta++;
                        }
                    }
                }

                var asd = ConvertirListaEnCadena(arbol.GetNode());
                if (asd == "2,3,4,5")
                {
                    sumatorioDeNumeroDeRamasVuelta = 0;
                }
            }

        }

        private static void Visualizador(MemoriaParaP151 memoria, NTree<List<int>> arbol, int numeroTotalDeRamas, int sumatorioDeNumeroDeRamasVuelta)
        {
            if ((memoria.listaMemoriaProbabilidades.Count > 1)&& (memoria.listaMemoriaProbabilidades.Count < 7))
            {
                Console.WriteLine("Nodo: ");
                foreach(var nodo in arbol.GetNode())
                {
                    Console.WriteLine(nodo);
                }
                Console.WriteLine("Numero de ramas totales: " + numeroTotalDeRamas);
                Console.WriteLine("Sumatorio de ramas de vuelta: " + sumatorioDeNumeroDeRamasVuelta);
            }
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
