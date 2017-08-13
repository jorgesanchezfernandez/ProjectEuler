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

            CalculateNextNode(ref NumTotalSingleSheets, ref NumTotalCuts, ref Tree);

            var solution = Double.Parse(NumTotalSingleSheets.ToString()) / Double.Parse(NumTotalCuts.ToString());

            // Para el contador e imprime el resultado:
            DateTime tiempo2 = DateTime.Now;
            TimeSpan total = new TimeSpan(tiempo2.Ticks - tiempo1.Ticks);
            Console.WriteLine("TIME: " + total.TotalSeconds);

            Console.WriteLine("TOTAL NUMBER OF SINGLES SHEETS: " + NumTotalSingleSheets);

            Console.WriteLine("TOTAL NUMBER OF CUTS: " + NumTotalCuts);

            Console.WriteLine("EXPECTED NUMBER: " + 12.47 * solution);

            //0.464399 solution
            Console.ReadKey();
        }

        public static bool CalculateNextNode (ref int NumSingleSheets, ref int NumTotalCuts,  ref NTree<List<int>> Tree)
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
                    Tree.numCutsInNodeToLeaf = 0;
                    Tree.numSinglesSheetsToLeaf = 0;
                    for (var i = Tree.GetNode().Count - 1; i >= 0; i--)
                    {
                        var newTree = new NTree<List<int>>(Tree.GenerateNewNode(i));
                        Tree.AddChild(newTree.GetNode());

                        CalculateNextNode(ref NumSingleSheets, ref NumTotalCuts, ref newTree);

                        Tree.numCutsInNodeToLeaf += newTree.numCutsInNodeToLeaf;
                        Tree.numSinglesSheetsToLeaf += newTree.numSinglesSheetsToLeaf;
                    }

                    if (Tree.IsSolitary)
                        Tree.numSinglesSheetsToLeaf++;

                    Tree.numCutsInNodeToLeaf++;

                    //Si sale el 2 ya podemos salir y si sale el 3 en la primera rama podemos salir
                    memory.Add(new MemoriaParaP151(Tree.GetNode(), Tree.numSinglesSheetsToLeaf, Tree.numCutsInNodeToLeaf));
                }
                else
                {
                    Tree.numCutsInNodeToLeaf++;
                    Tree.numSinglesSheetsToLeaf = 0;

                    //Si sale el 2 ya podemos salir y si sale el 3 en la primera rama podemos salir
                    memory.Add(new MemoriaParaP151(Tree.GetNode(), Tree.numSinglesSheetsToLeaf, Tree.numCutsInNodeToLeaf));
                }
                          
            }
            else
            {
                NumSingleSheets += savedNode.numSinglesSheetsToLeaf;

                NumTotalCuts += savedNode.numCutsInNodeToLeaf;

                Tree.numSinglesSheetsToLeaf = savedNode.numSinglesSheetsToLeaf;
                Tree.numCutsInNodeToLeaf = savedNode.numCutsInNodeToLeaf;
            }

            return found;
        }
    }
}
