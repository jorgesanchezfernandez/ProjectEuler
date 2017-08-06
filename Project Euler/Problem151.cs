using System.Collections.Generic;

namespace Project_Euler
{
    using System;
    using Utils;

    class Problem151
    {
        static void Main(string[] args)
        {
            var probabilidadesTotalPara = new List<double> { 0, 0, 0, 0 };
            var sumatorioDeLaprobabilidadDeLaRama = 1.0;

            var probabilidadesAcumuladaDelNodoPara = new List<double> { 1, 1, 1, 1 };
            var sumatorioDeProbabilidadDelNodoActual = 1.0;

            var numeroTotalDeRamas = 0;
            var arbol = new NTree<List<int>>(new List<int> { 2, 3, 4, 5 });
            var numeroSolitariosEncontrados = 0;
            var listaMemoriaProbabilidades = new Dictionary<List<int>, List<double>>();

            //Build the tree
            CalculoNuevoNivel(listaMemoriaProbabilidades, ref probabilidadesAcumuladaDelNodoPara, probabilidadesTotalPara, ref sumatorioDeProbabilidadDelNodoActual, ref sumatorioDeLaprobabilidadDeLaRama, arbol, ref numeroSolitariosEncontrados);
            //Traverse taken the probability
            //Check in the same level if the number it was already calculated
            //Save this whole number of this level in a list.
            var probabilidadTotal = probabilidadesTotalPara[0] + probabilidadesTotalPara[1] + probabilidadesTotalPara[2] + probabilidadesTotalPara[3];

            Console.WriteLine(probabilidadTotal / numeroTotalDeRamas);
            Console.ReadKey();
        }

        private static void CalculoNuevoNivel(Dictionary<List<int>, List<double>> listaMemoriaProbabilidades, ref List<double> probabilidadesAcumuladaDelNodoPara, List<double> probabilidadesTotalPara, ref double sumatorioDeProbabilidadDelNodoActual, ref double sumatorioDeLaprobabilidadDeLaRama, NTree<List<int>> arbol, ref int numeroSolitariosEncontrados)
        {
            // Cálculo de probabilidades por nivel y sumatorio de la rama
            var numeroElementosDelNodo = Convert.ToDouble(arbol.GetNode().Count);
            var probabilidadDelNivel = 1 / numeroElementosDelNodo;

            // Recorremos cada elemento del nodo
            for (var i = arbol.GetNode().Count - 1; i >= 0; i--)
            {
                if ((arbol.GetNode()[i] != 5) || ((arbol.GetNode()[i] == 5)&&(arbol.GetNode().Count > 1)))
                {
                    // Creamos el nuevo nodo por cada uno de los numeros
                    List<int> nuevoNodo = CrearNuevoNodo(arbol, i);

                    var nuevoArbol = new NTree<List<int>>(nuevoNodo);

                    if ((i == arbol.GetNode().Count - 1) || ((i < arbol.GetNode().Count - 1) && (arbol.GetNode()[i] != arbol.GetNode()[i + 1])) || !listaMemoriaProbabilidades.ContainsKey(nuevoNodo))
                    {
                        CalculoNuevoNivel(listaMemoriaProbabilidades, ref probabilidadesAcumuladaDelNodoPara, probabilidadesTotalPara, ref sumatorioDeProbabilidadDelNodoActual, ref sumatorioDeLaprobabilidadDeLaRama, nuevoArbol, ref numeroSolitariosEncontrados);
                        
                        // Sumamos un solitario encontrado
                        if ((nuevoArbol.GetNode().Count == 1)&&(nuevoArbol.GetNode()[0] != 5))
                        {
                            numeroSolitariosEncontrados++;
                        }
                        var numeroElementosDelNuevoNodo = Convert.ToDouble(nuevoArbol.GetNode().Count);
                        probabilidadesAcumuladaDelNodoPara[numeroSolitariosEncontrados] = probabilidadesAcumuladaDelNodoPara[numeroSolitariosEncontrados] * (1 / numeroElementosDelNuevoNodo);
                        listaMemoriaProbabilidades.Add(nuevoArbol.GetNode(), probabilidadesAcumuladaDelNodoPara);
                    }
                    else
                    {
                        var probabilidadGuardadaDeNodoRepetido = new List<double>();
                        if (listaMemoriaProbabilidades.TryGetValue(arbol.GetNode(), out probabilidadGuardadaDeNodoRepetido))
                        {
                            for (var z = 0; z <= 3; z++)
                            {
                                probabilidadesTotalPara[z] += probabilidadGuardadaDeNodoRepetido[z];
                            }
                        }
                    }
                }      
            }
            probabilidadesTotalPara[numeroSolitariosEncontrados] += probabilidadesAcumuladaDelNodoPara[numeroSolitariosEncontrados];
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
    }
}
