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

        public static HashMapPlus memory = new HashMapPlus();

        //public static List<List<double>> TotalProbabilityPerSinglesSheets = new List<List<double>>();

        public static List<double> TotalProbabilityPerSinglesSheets = new List<double> { 0.0, 0.0, 0.0, 0.0};
        static void Main(string[] args)
        {
            // Inicia el contador:
            DateTime tiempo1 = DateTime.Now;

            var NumTotalSingleSheets = 0;

            var probability = 1.0 ;

            CalculateNextNode(NumTotalSingleSheets, ref Tree, probability);

            var SumProbabilityPerSingleSheet = new List<double>();

            //for (var i = 1; i <= 3; i++)
            //{
            //    foreach (var element in TotalProbabilityPerSinglesSheets[i])
            //    {
            //        SumProbabilityPerSingleSheet[i] += element;
            //    }
            //}

            var expectedNumberOfTimes = 0.0;

            for (var i = 1; i <= 3; i++)
            {
                expectedNumberOfTimes += TotalProbabilityPerSinglesSheets[i] * i;
            }

            // Para el contador e imprime el resultado:
            DateTime tiempo2 = DateTime.Now;
            TimeSpan total = new TimeSpan(tiempo2.Ticks - tiempo1.Ticks);
            Console.WriteLine("TIME: " + total.TotalSeconds);

            Console.WriteLine("EXPECTED NUMBER: " + expectedNumberOfTimes);

            //0.464399 solution
            Console.ReadKey();
        }

        public static void CalculateNextNode (int NumSingleSheets, ref NTree<List<int>> Tree, double probability)
        {
            //var savedNode = memory.Find(Tree.GetNode());
            //var found = savedNode != null;

            //if (!found)
            //{
                if (Tree.IsSolitary)
                    NumSingleSheets++;

                if (!Tree.IsLeaf)
                {
                //for (var j = 0; j <= 3; j++)
                //{
                //    Tree.probabilityNodeToleafPerSinglesSheets[j] = new List<double> ();
                //}
                probability = probability * (1.0 / double.Parse(Tree.GetNode().Count.ToString()));
                for (var i = Tree.GetNode().Count - 1; i >= 0; i--)
                    {
                        var newTree = new NTree<List<int>>(Tree.GenerateNewNode(i));
                        Tree.AddChild(newTree.GetNode());

                        CalculateNextNode(NumSingleSheets, ref newTree, probability);

                        

                    //if (Tree.IsSolitary)
                    //{
                    //    Tree.probabilityNodeToleafPerSinglesSheets[NumSingleSheets].Add(probability);
                    //    for (var j = 0; j <= NumSingleSheets - 1; j++)
                    //    {
                    //        foreach (var element in newTree.probabilityNodeToleafPerSinglesSheets[j])
                    //        {
                    //            Tree.probabilityNodeToleafPerSinglesSheets[j + 1].Add(element);
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    for (var j = 0; j <= NumSingleSheets; j++)
                    //    {
                    //        foreach (var element in newTree.probabilityNodeToleafPerSinglesSheets[j])
                    //        {
                    //            Tree.probabilityNodeToleafPerSinglesSheets[j].Add(element);
                    //        }
                    //    }
                    //}

                }

                //Si sale el 2 ya podemos salir y si sale el 3 en la primera rama podemos salir
                //memory.Add(new MemoriaParaP151(Tree.GetNode(), Tree.probabilityNodeToleafPerSinglesSheets));
            }
                else
                {
                    TotalProbabilityPerSinglesSheets[NumSingleSheets] += probability;

                    //Tree.probabilityNodeToleafPerSinglesSheets[].Add(probability);

                    //Si sale el 2 ya podemos salir y si sale el 3 en la primera rama podemos salir
                    //memory.Add(new MemoriaParaP151(Tree.GetNode(), Tree.probabilityNodeToleafPerSinglesSheets));
                }
                          
            //}
            //else
            //{
            //    Tree.numBranchesPerQuantityOfSinglesSheets = savedNode.numBranchesPerQuantityOfSinglesSheets;
            //    Tree.numTotalBranches = savedNode.numTotalBranches;
            //    Tree.numCuts = savedNode.numCuts;
            //}

            //return found;
        }
    }
}
