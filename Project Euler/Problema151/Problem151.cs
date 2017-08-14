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

        public static int NumTotalTreeLeafs
        {
            get {
                    return NumTotalOfBranchesPerTimes [0] + NumTotalOfBranchesPerTimes [1] + NumTotalOfBranchesPerTimes [2] + NumTotalOfBranchesPerTimes [3];
                }
        }

        public static HashMapPlus memory = new HashMapPlus();

        public static double CeilingDividor = 2.0;

        static void Main(string[] args)
        {
            // Inicia el contador:
            DateTime tiempo1 = DateTime.Now;

            var NumTotalSingleSheets = 0;

            CalculateNextNode(ref NumTotalSingleSheets, ref Tree);

            var probabilityPer1SingleSheet = Double.Parse(NumTotalOfBranchesPerTimes[1].ToString()) / Double.Parse(NumTotalTreeLeafs.ToString());
            var probabilityPer2SingleSheet = Double.Parse(NumTotalOfBranchesPerTimes[2].ToString()) / Double.Parse(NumTotalTreeLeafs.ToString());
            var probabilityPer3SingleSheet = Double.Parse(NumTotalOfBranchesPerTimes[3].ToString()) / Double.Parse(NumTotalTreeLeafs.ToString());

            var expectedNumberOfTimes = probabilityPer1SingleSheet + 2 * probabilityPer2SingleSheet + 3 * probabilityPer3SingleSheet;

            // Para el contador e imprime el resultado:
            DateTime tiempo2 = DateTime.Now;
            TimeSpan total = new TimeSpan(tiempo2.Ticks - tiempo1.Ticks);
            Console.WriteLine("TIME: " + total.TotalSeconds);

            Console.WriteLine("TOTAL NUMBER OF BRANCHES: " + NumTotalTreeLeafs);

            Console.WriteLine("EXPECTED NUMBER: " + expectedNumberOfTimes);

            //0.464399 solution
            Console.ReadKey();
        }

        public static bool CalculateNextNode (ref int NumSingleSheets, ref NTree<List<int>> Tree)
        {
            var savedNode = memory.Find(Tree.GetNode());
            var found = savedNode != null;

            if (!found)
            {
                if (Tree.IsSolitary)
                    NumSingleSheets++;

                if (!Tree.IsLeaf)
                {
                    Tree.numBranchesPerQuantityOfSinglesSheets = new List<int> { 0, 0, 0, 0 };

                    for (var i = Tree.GetNode().Count - 1; i >= 0; i--)
                    {
                        var newTree = new NTree<List<int>>(Tree.GenerateNewNode(i));
                        Tree.AddChild(newTree.GetNode());

                        CalculateNextNode(ref NumSingleSheets, ref newTree);

                        for (var j = 0; j <= NumSingleSheets - 1; j++)
                        {
                            if (Tree.IsSolitary)
                            {
                                Tree.numBranchesPerQuantityOfSinglesSheets[j + 1] += newTree.numBranchesPerQuantityOfSinglesSheets[j];
                            }
                            else
                            {
                                Tree.numBranchesPerQuantityOfSinglesSheets[j] += newTree.numBranchesPerQuantityOfSinglesSheets[j];
                            }
                        }
                    }

                    //Si sale el 2 ya podemos salir y si sale el 3 en la primera rama podemos salir
                    memory.Add(new MemoriaParaP151(Tree.GetNode(), Tree.numBranchesPerQuantityOfSinglesSheets));
                }
                else
                {
                    Tree.numBranchesPerQuantityOfSinglesSheets[0]++;

                    //Si sale el 2 ya podemos salir y si sale el 3 en la primera rama podemos salir
                    memory.Add(new MemoriaParaP151(Tree.GetNode(), Tree.numBranchesPerQuantityOfSinglesSheets));
                }
                          
            }
            else
            {
                NumSingleSheets += savedNode;

                NumTotalCuts += savedNode.numCutsInNodeToLeaf;

                Tree.numSinglesSheetsToLeaf = savedNode.numSinglesSheetsToLeaf;
                Tree.numCutsInNodeToLeaf = savedNode.numCutsInNodeToLeaf;
            }

            return found;
        }
    }
}
