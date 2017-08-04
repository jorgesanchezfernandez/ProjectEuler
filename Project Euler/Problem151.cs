﻿using System.Collections.Generic;

namespace Project_Euler
{
    using System;
    using Utils;

    class Problem151
    {
        static void Main(string[] args)
        {
            var probabilidadesPara = new List<int> { 0, 0, 0, 0 };
            var sumatorioDeLaprobabilidadDeLaRama = 0;
            
            var numeroTotalDeRamas = 0;
            var arbol = new NTree<List<int>>(new List<int> { 2, 3, 4, 5 });
            var numeroSolitariosEncontrados = 0;

            //Build the tree
            for (var i = arbol.GetNode().Count; i >= 0; i--)
            {
                var sumatorioDeProbabilidadDesdeNodoHastaHoja = 0; // incompleto

                if (arbol.GetNode().Count == 1)
                {
                    if (arbol.GetNode()[i] == 5)
                    {
                        probabilidadesPara[numeroSolitariosEncontrados] += sumatorioDeLaprobabilidadDeLaRama;
                    }
                    else
                    {
                        numeroSolitariosEncontrados++;
                    }
                }
                else
                {
                    var probabilidadDelNivel = 1 / arbol.GetNode().Count;
                    sumatorioDeLaprobabilidadDeLaRama = sumatorioDeLaprobabilidadDeLaRama * probabilidadDelNivel;
                }

                var nuevoNodo = new List<int>();
                for (var j = 0; j < arbol.GetNode().Count; j++ )
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

                if (arbol.GetNode()[i] != arbol.GetNode()[i + 1])
                {
                    calculoNuevoNivel(arbol, sumatorioDeLaprobabilidadDeLaRama);
                }else
                {
                    //La misma probabilidad anterior
                }


            }
            //Traverse taken the probability
            //Check in the same level if the number it was already calculated
            //Save this whole number of this level in a list.
            var probabilidadTotal = probabilidadesPara[0] + probabilidadesPara[1] + probabilidadesPara[2] + probabilidadesPara[3];

            Console.WriteLine(probabilidadTotal / numeroTotalDeRamas);
            Console.ReadKey();
        }
    }
}
