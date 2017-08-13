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
        public static NTree<List<int>> Tree = new NTree<List<int>>(new List<int> { 2, 3, 4, 5 });

        public static int [] NumTotalOfBranchesPerTimes = new int[] { 0, 0, 0, 0 };

        public static ulong NumTotalTreeLeafs = 0;

        public static HashMapPlus memory = new HashMapPlus();

        public static double CeilingDividor = 2.0;

        static void Main(string[] args)
        {
            // Inicia el contador:
            DateTime tiempo1 = DateTime.Now;

            var NumTotalSingleSheets = 0;
            var NumTotalCuts = 1;

            var numSinglesSheetsToLeaf = 0;
            var numCutsInNodeToLeaf = 0;

            CalculateNextNode(ref NumTotalSingleSheets, ref NumTotalCuts, ref Tree, ref numSinglesSheetsToLeaf,ref numCutsInNodeToLeaf);

            var solution = Double.Parse(NumTotalSingleSheets.ToString()) / Double.Parse(NumTotalCuts.ToString());

            // Para el contador e imprime el resultado:
            DateTime tiempo2 = DateTime.Now;
            TimeSpan total = new TimeSpan(tiempo2.Ticks - tiempo1.Ticks);
            Console.WriteLine("TIEMPO: " + total.TotalSeconds);

            Console.WriteLine("TOTAL NUMBER OF CUTS: " + NumTotalCuts);

            Console.WriteLine("EXPECTED NUMBER: " + solution);

            //0.464399 solution
            Console.ReadKey();
        }

        public static bool CalculateNextNode (ref int NumSingleSheets, ref int NumTotalCuts,  ref NTree<List<int>> Tree, ref int numSinglesSheetsToLeaf,ref int numCutsInNodeToLeaf)
        {
            var savedNode = memory.Find(Tree.GetNode());
            var found = savedNode != null;

            if (!found)
            {
                if (Tree.IsSolitary)
                    NumSingleSheets++;

                NumTotalCuts++;

                if (!Tree.IsLeaf)
                {
                    numCutsInNodeToLeaf = 0;
                    numSinglesSheetsToLeaf = 0;
                    for (var i = Tree.GetNode().Count - 1; i >= 0; i--)
                    {
                        var newTree = new NTree<List<int>>(Tree.GenerateNewNode(i));

                        CalculateNextNode(ref NumSingleSheets, ref NumTotalCuts, ref newTree, ref numSinglesSheetsToLeaf,ref numCutsInNodeToLeaf);
                    }
                    //Buscar los valores del nodo inmediato para recuperar los valores de cada nodo

                    if (Tree.IsSolitary)
                        numSinglesSheetsToLeaf = NumSingleSheets - (NumSingleSheets - 1);

                    numCutsInNodeToLeaf++;

                    //Si sale el 2 ya podemos salir y si sale el 3 en la primera rama podemos salir
                    memory.Add(new MemoriaParaP151(Tree.GetNode(), numSinglesSheetsToLeaf, numCutsInNodeToLeaf));
                }
                else
                {
                    numCutsInNodeToLeaf++;

                    //Si sale el 2 ya podemos salir y si sale el 3 en la primera rama podemos salir
                    memory.Add(new MemoriaParaP151(Tree.GetNode(), 0, numCutsInNodeToLeaf));
                }
                          
            }
            else
            {
                NumSingleSheets += savedNode.numSinglesSheetsToLeaf;

                NumTotalCuts += savedNode.numCutsInNodeToLeaf;

                numSinglesSheetsToLeaf += savedNode.numSinglesSheetsToLeaf;
                numCutsInNodeToLeaf += savedNode.numCutsInNodeToLeaf;
            }

            return found;
        }
    }
}
