using System.Collections.Generic;

namespace Project_Euler
{
    using Problema151;
    using System;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;

    using Utils;

    class Problem151
    {
        public static ulong numeroTotalDeTerminacionesMenosHoja = 0;

        public static int sumaDeSolitariosPorRama = 0;

        public static int sumaDeSolitariosDesdeNodoHastaHoja = 0;

        public static int[] guardaNumeroDeRamasPorCantidadDeSolitarios = new int[] { 0, 0, 0, 0 };

        public static List<MemoriaParaP151> memoria = new List<MemoriaParaP151>();

        public static double CeilingDividor = 2.0;
 
        static void Main(string[] args)
        {
            // Inicia el contador:
            DateTime tiempo1 = DateTime.Now;
            
            var arbol = new NTree<List<int>>(new List<int> { 2, 3, 4, 5 });

            CalcularNuevaRama(arbol, sumaDeSolitariosPorRama, sumaDeSolitariosDesdeNodoHastaHoja);

            // Para el contador e imprime el resultado:
            DateTime tiempo2 = DateTime.Now;
            TimeSpan total = new TimeSpan(tiempo2.Ticks - tiempo1.Ticks);
            Console.WriteLine("TIEMPO: " + total.TotalSeconds);

            var media = 0.0;
            for (var j = 0; j <= arbol.guardaNumeroDeRamasPorCantidadDeSolitariosEnNodoHastaHoja.Length - 1; j++)
            {
                Console.WriteLine("TOTAL NUMERO DE "+ j +" SOLITARIOS: " + arbol.guardaNumeroDeRamasPorCantidadDeSolitariosEnNodoHastaHoja[j]);
                var numTotal = arbol.guardaNumeroDeRamasPorCantidadDeSolitariosEnNodoHastaHoja[j];
                media += double.Parse(numTotal.ToString()) / double.Parse(numeroTotalDeTerminacionesMenosHoja.ToString());
            }
            
            Console.WriteLine("TOTAL NUMERO DE RAMAS: " + numeroTotalDeTerminacionesMenosHoja);

            Console.WriteLine("MEDIA: " + media);

            Console.ReadKey();
        }

        private static int CalcularNuevaRama(NTree<List<int>> arbol, int sumaDeSolitariosPorRama, int sumaDeSolitariosDesdeNodoHastaHoja)
        {
            int indice = BuscarNodoEnLista(arbol);

            if (indice == -1)
            {
                if (!arbol.IsLeaf)
                {
                    if (arbol.EsSolitario)
                    {
                        sumaDeSolitariosPorRama++;
                    }
                    //Sumar el numero de solitarios encontrados
                    ulong numeroDeHojasDelNodo = 0;
                    for (var i = arbol.GetNode().Count - 1; i >= 0; i--)
                    {
                        var nuevoArbol = new NTree<List<int>>(CrearNuevoNodo(arbol, i));
                        if (CalcularNuevaRama(nuevoArbol, sumaDeSolitariosPorRama, sumaDeSolitariosDesdeNodoHastaHoja) == -1)
                        {
                            if (arbol.EsSolitario)
                            {
                                arbol.guardaNumeroDeRamasPorCantidadDeSolitariosEnNodoHastaHoja[0] = 0;
                                for (var z = 1; z <= arbol.guardaNumeroDeRamasPorCantidadDeSolitariosEnNodoHastaHoja.Length - 1; z++)
                                {
                                    if (nuevoArbol.guardaNumeroDeRamasPorCantidadDeSolitariosEnNodoHastaHoja[z] > 0)
                                    {
                                        arbol.guardaNumeroDeRamasPorCantidadDeSolitariosEnNodoHastaHoja[z+1] = 1;
                                    }
                                    
                                }
                            }
                            else
                            {
                                for (var z = 0; z <= arbol.guardaNumeroDeRamasPorCantidadDeSolitariosEnNodoHastaHoja.Length - 1; z++)
                                {
                                    arbol.guardaNumeroDeRamasPorCantidadDeSolitariosEnNodoHastaHoja[z] += nuevoArbol.guardaNumeroDeRamasPorCantidadDeSolitariosEnNodoHastaHoja[z];
                                }
                            }                          
                        }
                        numeroDeHojasDelNodo += nuevoArbol.numeroDeHojasDelNodo;
                    }
                    //Añadir a la lista
                    memoria.Add(new MemoriaParaP151(arbol.GetNode(), arbol.guardaNumeroDeRamasPorCantidadDeSolitariosEnNodoHastaHoja, numeroDeHojasDelNodo));
                }
                else
                {
                    guardaNumeroDeRamasPorCantidadDeSolitarios[sumaDeSolitariosPorRama]++;

                    if (arbol.EsSolitario)
                    {
                        arbol.guardaNumeroDeRamasPorCantidadDeSolitariosEnNodoHastaHoja[1]++;
                    }else
                    {
                        arbol.guardaNumeroDeRamasPorCantidadDeSolitariosEnNodoHastaHoja[0]++;
                    }
                    arbol.numeroDeHojasDelNodo ++;

                    memoria.Add(new MemoriaParaP151(arbol.GetNode(), arbol.guardaNumeroDeRamasPorCantidadDeSolitariosEnNodoHastaHoja, arbol.numeroDeHojasDelNodo));
                    //Sumar uno 
                    numeroTotalDeTerminacionesMenosHoja++;
                }
            }
            else
            {
                numeroTotalDeTerminacionesMenosHoja += memoria[indice].numeroDeHojasDelNodo;

                for (var z = 0; z <= arbol.guardaNumeroDeRamasPorCantidadDeSolitariosEnNodoHastaHoja.Length - 1; z++)
                {
                    if (memoria[indice].guardaNumeroDeRamasPorCantidadDeSolitariosEnNodo[z] > 0)
                    {
                        guardaNumeroDeRamasPorCantidadDeSolitarios[sumaDeSolitariosPorRama + z]+= memoria[indice].guardaNumeroDeRamasPorCantidadDeSolitariosEnNodo[z];
                    }
                }
            }

            return indice;
        }

        private static int BuscarNodoEnLista(NTree<List<int>> arbol)
        {
            var indice = -1;
            var encontrado = false;

            for (var i = memoria.Count - 1; i > -1 && memoria[i].nodo.Count >= arbol.GetNode().Count && i >= Math.Ceiling(memoria.Count / CeilingDividor) && !encontrado; i--)
            {
                if (memoria[i].nodo.Count == arbol.GetNode().Count)
                {
                    var iguales = true;
                    for (var j = 0; j <= memoria[i].nodo.Count - 1 && iguales; j++)
                    {
                        iguales = memoria[i].nodo[j] == arbol.GetNode()[j];
                    }
                    encontrado = iguales;
                    indice = encontrado == true ? i : -1;
                }
            }
            
            return indice;
        }

        //private static void CalculoNuevoNivel(ref MemoriaParaP151 memoria, double sumatorioDeLaprobabilidadDeLaRama, ref List<double> probabilidadesAcumuladaDelNodoPara, NTree<List<int>> arbol, ref int numeroSolitariosEncontradosIda, ref int numeroSolitariosEncontradosVuelta, ref int numHojasPorNivel)
        //{
        //    Visualizador(memoria, arbol, numeroTotalDeRamas);
        //    numHojasPorNivel = 0;
        //    Visualizador(memoria, arbol, numeroTotalDeRamas);
        //     Cálculo de probabilidades por nivel y sumatorio de la rama
        //    var numeroElementosDelNodo = Convert.ToDouble(arbol.GetNode().Count);
        //    var probabilidadDelNivel = 1 / numeroElementosDelNodo;
        //    sumatorioDeLaprobabilidadDeLaRama = sumatorioDeLaprobabilidadDeLaRama * probabilidadDelNivel;

        //    if (!EsHojaTrueYTrataNumeroSolitario(sumatorioDeLaprobabilidadDeLaRama, arbol, numeroSolitariosEncontradosIda, ref numHojasPorNivel))
        //    {                
        //         Recorremos cada elemento del nodo
        //        for (var i = arbol.GetNode().Count - 1; i >= 0; i--)
        //        {
        //            NTree<List<int>> nuevoArbol = ObtenerNuevoNodo(arbol, i);

        //            if (NoHemosCalculadoElNuevoNodoPreviamente(memoria, arbol, i, nuevoArbol))
        //            {
        //                CalculoNuevoNivel(ref memoria, sumatorioDeLaprobabilidadDeLaRama, ref probabilidadesAcumuladaDelNodoPara, nuevoArbol, ref numeroSolitariosEncontradosIda, ref numeroSolitariosEncontradosVuelta, ref numHojasPorNivel);

        //                GuardaSolitariosDelNuevoNodo(memoria, nuevoArbol);
        //                GuardaProbabilidadesDelNuevoNodo(memoria, nuevoArbol);
        //                GuardaNumeroDeHojasDelNuevoNodo(memoria, nuevoArbol);

        //            }
        //            else
        //            {
        //                numHojasPorNivel = TomaValoresGuardadosEnMemoria(memoria, nuevoArbol);
        //            }
        //        }
        //    }
        //}

        //private static int TomaValoresGuardadosEnMemoria(MemoriaParaP151 memoria, NTree<List<int>> nuevoArbol)
        //{
        //    int numHojasPorNivel;
        //    var probabilidadGuardadaDeNodoRepetido = new List<double>();

        //    if (memoria.listaMemoriaProbabilidades.TryGetValue(ConvertirListaEnCadena(nuevoArbol.GetNode()), out probabilidadGuardadaDeNodoRepetido))
        //    {
        //        for (var z = 0; z <= 3; z++)
        //        {
        //            probabilidadesTotalPara[z] += probabilidadGuardadaDeNodoRepetido[z];

        //        }
        //    }
        //    if (memoria.hojasTotales.TryGetValue(ConvertirListaEnCadena(nuevoArbol.GetNode()), out numHojasPorNivel))
        //    {
        //        numeroTotalDeRamas += numHojasPorNivel;
        //    }

        //    return numHojasPorNivel;
        //}

        //private static void GuardaSolitariosDelNuevoNodo(MemoriaParaP151 memoria, NTree<List<int>> nuevoArbol)
        //{
        //    var sumaDeSolitariosPorNivel = 0;
        //    var numDeSolitariosPorNivel = 0;

        //    if (!memoria.hojasTotales.ContainsKey(ConvertirListaEnCadena(nuevoArbol.GetNode())))
        //    {
        //        for (var j = nuevoArbol.GetNode().Count - 1; j >= 0; j--)
        //        {
        //            NTree<List<int>> ramasAnteriores = ObtenerNuevoNodo(nuevoArbol, j);
        //            if ((ramasAnteriores.GetNode().Count > 0) && (memoria.numeroDeSoliarios.TryGetValue(ConvertirListaEnCadena(ramasAnteriores.GetNode()), out numDeSolitariosPorNivel)))
        //            {
        //                sumaDeSolitariosPorNivel += numDeSolitariosPorNivel;
        //            }
        //        }

        //        if ((nuevoArbol.GetNode().Count == 1) && (nuevoArbol.GetNode()[0] != 5))
        //        {
        //            sumaDeSolitariosPorNivel++;
        //        }

        //        memoria.numeroDeSoliarios.Add(ConvertirListaEnCadena(nuevoArbol.GetNode()), sumaDeSolitariosPorNivel);
        //    }

        //}
        //private static void GuardaProbabilidadesDelNuevoNodo(MemoriaParaP151 memoria, NTree<List<int>> nuevoArbol)
        //{
        //    var probabilidadesTotalesPorNivel = new List<double> { 1, 1, 1, 1 };
        //    var numDeSolitariosPorNivel = 0;
        //    var numeroElementosDelNodo = Convert.ToDouble(nuevoArbol.GetNode().Count);

        //    if (!memoria.listaMemoriaProbabilidades.ContainsKey(ConvertirListaEnCadena(nuevoArbol.GetNode())))
        //    {
        //        for (var j = nuevoArbol.GetNode().Count - 1; j >= 0; j--)
        //        {
        //            NTree<List<int>> ramasAnteriores = ObtenerNuevoNodo(nuevoArbol, j);
        //            if ((ramasAnteriores.GetNode().Count > 0) && (memoria.listaMemoriaProbabilidades.TryGetValue(ConvertirListaEnCadena(ramasAnteriores.GetNode()), out probabilidadesTotalesPorNivel)))
        //            {
        //                if (memoria.numeroDeSoliarios.TryGetValue(ConvertirListaEnCadena(ramasAnteriores.GetNode()), out numDeSolitariosPorNivel))
        //                {

        //                    probabilidadesTotalesPorNivel[numDeSolitariosPorNivel] = probabilidadesTotalesPorNivel[numDeSolitariosPorNivel] * 1 / numeroElementosDelNodo;
        //                }
        //            }
        //            else
        //            {
        //                memoria.numeroDeSoliarios.TryGetValue(ConvertirListaEnCadena(ramasAnteriores.GetNode()), out numDeSolitariosPorNivel);
        //                probabilidadesTotalesPorNivel[numDeSolitariosPorNivel] = 1 / numeroElementosDelNodo;

        //            }
        //        }
        //        memoria.listaMemoriaProbabilidades.Add(ConvertirListaEnCadena(nuevoArbol.GetNode()), probabilidadesTotalesPorNivel);
        //    }

        //}


        //private static void GuardaNumeroDeHojasDelNuevoNodo(MemoriaParaP151 memoria, NTree<List<int>> nuevoArbol)
        //{
        //    var sumaHojasPorNivel = 0;
        //    var numHojasPorNivel = 0;
        //    if (!memoria.hojasTotales.ContainsKey(ConvertirListaEnCadena(nuevoArbol.GetNode())))
        //    {
        //        for (var j = nuevoArbol.GetNode().Count - 1; j >= 0; j--)
        //        {
        //            NTree<List<int>> ramasAnteriores = ObtenerNuevoNodo(nuevoArbol, j);
        //            if ((ramasAnteriores.GetNode().Count > 0) && (memoria.hojasTotales.TryGetValue(ConvertirListaEnCadena(ramasAnteriores.GetNode()), out numHojasPorNivel)))
        //            {
        //                sumaHojasPorNivel += numHojasPorNivel;
        //            }
        //            else
        //            {
        //                sumaHojasPorNivel++;
        //            }
        //        }
        //        memoria.hojasTotales.Add(ConvertirListaEnCadena(nuevoArbol.GetNode()), sumaHojasPorNivel);
        //    }
        //}

        //private static NTree<List<int>> ObtenerNuevoNodo(NTree<List<int>> arbol, int i)
        //{
        //     Creamos el nuevo nodo por cada uno de los numeros
        //    List<int> nuevoNodo = CrearNuevoNodo(arbol, i);
        //    var nuevoArbol = new NTree<List<int>>(nuevoNodo);
        //    return nuevoArbol;
        //}

        //private static bool NoHemosCalculadoElNuevoNodoPreviamente(MemoriaParaP151 memoria, NTree<List<int>> arbol, int i, NTree<List<int>> nuevoArbol)
        //{
        //    return !memoria.listaMemoriaProbabilidades.ContainsKey(ConvertirListaEnCadena(nuevoArbol.GetNode())) && (i == arbol.GetNode().Count - 1 || (i < arbol.GetNode().Count - 1) && (arbol.GetNode()[i] != arbol.GetNode()[i + 1]));
        //}

        //private static bool EsHojaTrueYTrataNumeroSolitario(double sumatorioDeLaprobabilidadDeLaRama, NTree<List<int>> arbol, int numeroSolitariosEncontradosIda, ref int numeroTotalDeHojasNivel)
        //{
        //    if (arbol.GetNode().Count == 1)
        //    {
        //        if (arbol.GetNode()[0] == 5)
        //        {
        //            numeroTotalDeRamas++;
        //            probabilidadesTotalPara[numeroSolitariosEncontradosIda] += sumatorioDeLaprobabilidadDeLaRama;
        //            return true;
        //        }
        //        numeroSolitariosEncontradosIda++;
        //    }
        //    return false;
        //}

        //private static void Visualizador(MemoriaParaP151 memoria, NTree<List<int>> arbol, int numeroTotalDeRamas)
        //{
        //    if (memoria.listaMemoriaProbabilidades.Count < 7)
        //    {
        //        Console.WriteLine("Nodo: ");
        //        foreach (var nodo in arbol.GetNode())
        //        {
        //            Console.WriteLine(nodo);
        //        }
        //        Console.WriteLine("Numero de ramas totales: " + numeroTotalDeRamas);
        //        var numeroDeHojasDelNodo = 0;
        //        memoria.hojasTotales.TryGetValue(ConvertirListaEnCadena(arbol.GetNode()), out numeroDeHojasDelNodo);
        //        Console.WriteLine("Sumatorio de ramas de vuelta: " + numeroDeHojasDelNodo);
        //    }
        //}

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

        //public static string ConvertirListaEnCadena(List<int> nodo)
        //{
        //    var cadena = "";
        //    for (var i = 0; i < nodo.Count; i++)
        //    {
        //        if (i == 0)
        //        {
        //            cadena += nodo[i].ToString();
        //        }
        //        else
        //        {
        //            cadena += "," + nodo[i].ToString();
        //        }
        //    }
        //    return cadena;
        //}
    }
}
