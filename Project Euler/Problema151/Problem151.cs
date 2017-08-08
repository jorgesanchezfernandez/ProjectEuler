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

            var memoria = new MemoriaParaP151();

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
            Visualizador(memoria, arbol, numeroTotalDeRamas);
            numHojasPorNivel = 0;
            //Visualizador(memoria, arbol, numeroTotalDeRamas);
            // Cálculo de probabilidades por nivel y sumatorio de la rama
            var numeroElementosDelNodo = Convert.ToDouble(arbol.GetNode().Count);
            var probabilidadDelNivel = 1 / numeroElementosDelNodo;
            sumatorioDeLaprobabilidadDeLaRama = sumatorioDeLaprobabilidadDeLaRama * probabilidadDelNivel;

            if (!EsHojaTrueYTrataNumeroSolitario(sumatorioDeLaprobabilidadDeLaRama, arbol, numeroSolitariosEncontradosIda, ref numHojasPorNivel))
            {                
                // Recorremos cada elemento del nodo
                for (var i = arbol.GetNode().Count - 1; i >= 0; i--)
                {
                    NTree<List<int>> nuevoArbol = ObtenerNuevoNodo(arbol, i);

                    if (NoHemosCalculadoElNuevoNodoPreviamente(memoria, arbol, i, nuevoArbol))
                    {
                        CalculoNuevoNivel(ref memoria, sumatorioDeLaprobabilidadDeLaRama, ref probabilidadesAcumuladaDelNodoPara, nuevoArbol, ref numeroSolitariosEncontradosIda, ref numeroSolitariosEncontradosVuelta, ref numHojasPorNivel);

                        GuardaSolitariosDelNuevoNodo(memoria, nuevoArbol);
                        GuardaProbabilidadesDelNuevoNodo(memoria, nuevoArbol);
                        GuardaNumeroDeHojasDelNuevoNodo(memoria, nuevoArbol);

                    }
                    else
                    {
                        numHojasPorNivel = TomaValoresGuardadosEnMemoria(memoria, nuevoArbol);
                    }
                }
            }
        }

        private static int TomaValoresGuardadosEnMemoria(MemoriaParaP151 memoria, NTree<List<int>> nuevoArbol)
        {
            int numHojasPorNivel;
            var probabilidadGuardadaDeNodoRepetido = new List<double>();

            if (memoria.listaMemoriaProbabilidades.TryGetValue(ConvertirListaEnCadena(nuevoArbol.GetNode()), out probabilidadGuardadaDeNodoRepetido))
            {
                for (var z = 0; z <= 3; z++)
                {
                    probabilidadesTotalPara[z] += probabilidadGuardadaDeNodoRepetido[z];

                }
            }
            if (memoria.hojasTotales.TryGetValue(ConvertirListaEnCadena(nuevoArbol.GetNode()), out numHojasPorNivel))
            {
                numeroTotalDeRamas += numHojasPorNivel;
            }

            return numHojasPorNivel;
        }

        private static void GuardaSolitariosDelNuevoNodo(MemoriaParaP151 memoria, NTree<List<int>> nuevoArbol)
        {
            var sumaDeSolitariosPorNivel = 0;
            var numDeSolitariosPorNivel = 0;

            if (!memoria.hojasTotales.ContainsKey(ConvertirListaEnCadena(nuevoArbol.GetNode())))
            {
                for (var j = nuevoArbol.GetNode().Count - 1; j >= 0; j--)
                {
                    NTree<List<int>> ramasAnteriores = ObtenerNuevoNodo(nuevoArbol, j);
                    if ((ramasAnteriores.GetNode().Count > 0 ) && (memoria.numeroDeSoliarios.TryGetValue(ConvertirListaEnCadena(ramasAnteriores.GetNode()), out numDeSolitariosPorNivel)))
                    {
                        sumaDeSolitariosPorNivel += numDeSolitariosPorNivel;
                    }
                }

                if ((nuevoArbol.GetNode().Count == 1) && (nuevoArbol.GetNode()[0] != 5))
                {
                    sumaDeSolitariosPorNivel++;
                }

                memoria.numeroDeSoliarios.Add(ConvertirListaEnCadena(nuevoArbol.GetNode()), sumaDeSolitariosPorNivel);
            }

        }
        private static void GuardaProbabilidadesDelNuevoNodo(MemoriaParaP151 memoria, NTree<List<int>> nuevoArbol)
        {
            var probabilidadesTotalesPorNivel = new List<double> { 1, 1, 1, 1};
            var numDeSolitariosPorNivel = 0;
            var numeroElementosDelNodo = Convert.ToDouble(nuevoArbol.GetNode().Count);

            if (!memoria.listaMemoriaProbabilidades.ContainsKey(ConvertirListaEnCadena(nuevoArbol.GetNode())))
            {
                for (var j = nuevoArbol.GetNode().Count - 1; j >= 0; j--)
                {
                    NTree<List<int>> ramasAnteriores = ObtenerNuevoNodo(nuevoArbol, j);
                    if ((ramasAnteriores.GetNode().Count > 0) && (memoria.listaMemoriaProbabilidades.TryGetValue(ConvertirListaEnCadena(ramasAnteriores.GetNode()), out probabilidadesTotalesPorNivel)))
                    {
                        if (memoria.numeroDeSoliarios.TryGetValue(ConvertirListaEnCadena(ramasAnteriores.GetNode()), out numDeSolitariosPorNivel))
                        {
                            
                            probabilidadesTotalesPorNivel[numDeSolitariosPorNivel] = probabilidadesTotalesPorNivel[numDeSolitariosPorNivel] * 1 / numeroElementosDelNodo;
                        }
                    }
                    else
                    {
                        memoria.numeroDeSoliarios.TryGetValue(ConvertirListaEnCadena(ramasAnteriores.GetNode()), out numDeSolitariosPorNivel);                      
                        probabilidadesTotalesPorNivel[numDeSolitariosPorNivel] = 1 / numeroElementosDelNodo;
                        
                    }
                }
                memoria.listaMemoriaProbabilidades.Add(ConvertirListaEnCadena(nuevoArbol.GetNode()), probabilidadesTotalesPorNivel);
            }

        }


        private static void GuardaNumeroDeHojasDelNuevoNodo(MemoriaParaP151 memoria,  NTree<List<int>> nuevoArbol)
        {
            var sumaHojasPorNivel = 0;
            var numHojasPorNivel = 0;
            if (!memoria.hojasTotales.ContainsKey(ConvertirListaEnCadena(nuevoArbol.GetNode())))
            {
                for (var j = nuevoArbol.GetNode().Count - 1; j >= 0; j--)
                {
                    NTree<List<int>> ramasAnteriores = ObtenerNuevoNodo(nuevoArbol, j);
                    if ((ramasAnteriores.GetNode().Count > 0) && (memoria.hojasTotales.TryGetValue(ConvertirListaEnCadena(ramasAnteriores.GetNode()), out numHojasPorNivel)))
                    {
                        sumaHojasPorNivel += numHojasPorNivel;
                    }
                    else
                    {
                        sumaHojasPorNivel++;
                    }
                }
                memoria.hojasTotales.Add(ConvertirListaEnCadena(nuevoArbol.GetNode()), sumaHojasPorNivel);
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
            return !memoria.listaMemoriaProbabilidades.ContainsKey(ConvertirListaEnCadena(nuevoArbol.GetNode())) && ( i == arbol.GetNode().Count - 1 || (i < arbol.GetNode().Count - 1) && (arbol.GetNode()[i] != arbol.GetNode()[i + 1]));
        }

        private static bool EsHojaTrueYTrataNumeroSolitario(double sumatorioDeLaprobabilidadDeLaRama, NTree<List<int>> arbol, int numeroSolitariosEncontradosIda, ref int numeroTotalDeHojasNivel)
        {
            if (arbol.GetNode().Count == 1)
            {
                if (arbol.GetNode()[0] == 5)
                {
                    numeroTotalDeRamas++;
                    probabilidadesTotalPara[numeroSolitariosEncontradosIda] += sumatorioDeLaprobabilidadDeLaRama;
                    return true;
                }
                    numeroSolitariosEncontradosIda++;
            }
            return false;
        }

        private static void Visualizador(MemoriaParaP151 memoria, NTree<List<int>> arbol, int numeroTotalDeRamas)
        {
            if (memoria.listaMemoriaProbabilidades.Count < 7)
            {
                Console.WriteLine("Nodo: ");
                foreach (var nodo in arbol.GetNode())
                {
                    Console.WriteLine(nodo);
                }
                Console.WriteLine("Numero de ramas totales: " + numeroTotalDeRamas);
                var numeroDeHojasDelNodo = 0;
                memoria.hojasTotales.TryGetValue(ConvertirListaEnCadena(arbol.GetNode()), out numeroDeHojasDelNodo);
                Console.WriteLine("Sumatorio de ramas de vuelta: " + numeroDeHojasDelNodo);
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
