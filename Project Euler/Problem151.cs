using System.Collections.Generic;

namespace Project_Euler
{
    using System;
    using Utils;

    class Problem151
    {
        static void Main(string[] args)
        {
            var probabilidadParaCero = 0;
            var probabilidadParaUno = 0;
            var probalidadParaDos = 0;
            var probalidadParaTres = 0;
            var numeroTotalDeRamas = 0;
            var arbol = new NTree<List<int>>(new List<int> { 2, 3, 4, 5 });

            //Build the tree
            for ( var i = 0; i < arbol.GetNode().Count; i++ )
            {
                var probabilidadDelNivel = 1 / arbol.GetNode().Count;

                var nuevoNodo = new List<int>();
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

                for (var j = 0; j < arbol.GetNode().Count; j++ )
                {
                    if (j != i)
                    {
                        nuevoNodo.Add(arbol.GetNode()[j]);
                    }
                }
                if (arbol.GetNode()[i] != arbol.GetNode()[i + 1])
                {
                    calculoNuevoNivel(arbol);
                }else
                {
                    //La misma probabilidad anterior
                }
            }
            //Traverse taken the probability
            //Check in the same level if the number it was already calculated
            //Save this whole number of this level in a list.
            var probabilidadTotal = probabilidadParaCero + probabilidadParaUno + probalidadParaDos + probalidadParaTres;

            Console.WriteLine(probabilidadTotal / numeroTotalDeRamas);
            Console.ReadKey();
        }
    }
}
