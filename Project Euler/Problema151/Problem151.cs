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
        public static NTree<List<int>> Tree = new NTree<List<int>>(new List<int> {2, 3, 4, 5 });

        public static HashMapPlus memory = new HashMapPlus();

        public static List<double> TotalProbabilityPerSinglesSheets = new List<double> { 0.0, 0.0, 0.0, 0.0};

        static void Main(string[] args)
        {
            // Inicia el contador:
            DateTime tiempo1 = DateTime.Now;

            var NumTotalSingleSheets = 0;

            var probability = 1.0 ;

            CalculateNextNode(NumTotalSingleSheets, ref Tree, probability);

            var SumProbabilityPerSingleSheet = new List<double>();

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

        public static bool CalculateNextNode (int NumSingleSheets, ref NTree<List<int>> Tree, double probability)
        {
            var savedNode = memory.Find(Tree.GetNode());
            var found = savedNode != null;

            if (!found)
            {         
                if (Tree.IsSolitary)
                    NumSingleSheets++;

                if (!Tree.IsLeaf)
                {
                    var nodeProbability = (1.0 / double.Parse(Tree.GetNode().Count.ToString()));
                    probability = probability * nodeProbability;

                    for (var i = Tree.GetNode().Count - 1; i >= 0; i--)
                    {
                        var newTree = new NTree<List<int>>(Tree.GenerateNewNode(i));

                        CalculateNextNode(NumSingleSheets, ref newTree, probability);

                        if (Tree.IsSolitary)
                        {
                            for (var j = 0; j <= NumSingleSheets - 1; j++)
                            {
                                foreach (var element in newTree.probabilityNodeToleafPer0SinglesSheets)
                                {
                                    Tree.probabilityNodeToleafPer1SinglesSheets.Add(element);
                                }
                                foreach (var element in newTree.probabilityNodeToleafPer1SinglesSheets)
                                {
                                    Tree.probabilityNodeToleafPer2SinglesSheets.Add(element);
                                }
                                foreach (var element in newTree.probabilityNodeToleafPer2SinglesSheets)
                                {
                                    Tree.probabilityNodeToleafPer3SinglesSheets.Add(element);
                                }
                            }
                        }
                        else
                        {
                            for (var j = 0; j <= NumSingleSheets; j++)
                            {
                                foreach (var element in newTree.probabilityNodeToleafPer0SinglesSheets)
                                {
                                    Tree.probabilityNodeToleafPer0SinglesSheets.Add(element);
                                }
                                foreach (var element in newTree.probabilityNodeToleafPer1SinglesSheets)
                                {
                                    Tree.probabilityNodeToleafPer1SinglesSheets.Add(element);
                                }
                                foreach (var element in newTree.probabilityNodeToleafPer2SinglesSheets)
                                {
                                    Tree.probabilityNodeToleafPer2SinglesSheets.Add(element);
                                }
                                foreach (var element in newTree.probabilityNodeToleafPer3SinglesSheets)
                                {
                                    Tree.probabilityNodeToleafPer3SinglesSheets.Add(element);
                                }
                            }
                        }
                        switch (NumSingleSheets)
                        {
                            case 0:
                                var listAux0 = new List<double>();
                                foreach (var element in Tree.probabilityNodeToleafPer0SinglesSheets)
                                {
                                    var newValue = element * nodeProbability;
                                    listAux0.Add(newValue);
                                }
                                Tree.probabilityNodeToleafPer0SinglesSheets = listAux0;
                                break;
                            case 1:
                                var listAux1 = new List<double>();
                                foreach (var element in Tree.probabilityNodeToleafPer1SinglesSheets)
                                {
                                    var newValue = element * nodeProbability;
                                    listAux1.Add(newValue);
                                }
                                Tree.probabilityNodeToleafPer0SinglesSheets = listAux1;
                                break;
                            case 2:
                                var listAux2 = new List<double>();
                                foreach (var element in Tree.probabilityNodeToleafPer1SinglesSheets)
                                {
                                    var newValue = element * nodeProbability;
                                    listAux2.Add(newValue);
                                }
                                Tree.probabilityNodeToleafPer0SinglesSheets = listAux2;
                                break;
                            case 3:
                                var listAux3 = new List<double>();
                                foreach (var element in Tree.probabilityNodeToleafPer1SinglesSheets)
                                {
                                    var newValue = element * nodeProbability;
                                    listAux3.Add(newValue);
                                }
                                Tree.probabilityNodeToleafPer0SinglesSheets = listAux3;
                                break;
                        }                     
                    }

                    memory.Add(new MemoriaParaP151(Tree.GetNode(), Tree.probabilityNodeToleafPer0SinglesSheets, Tree.probabilityNodeToleafPer1SinglesSheets, Tree.probabilityNodeToleafPer2SinglesSheets, Tree.probabilityNodeToleafPer3SinglesSheets));
                }
                else
                {
                    TotalProbabilityPerSinglesSheets[NumSingleSheets] += probability;

                    Tree.probabilityNodeToleafPer0SinglesSheets.Add(1.0);

                    memory.Add(new MemoriaParaP151(Tree.GetNode(), Tree.probabilityNodeToleafPer0SinglesSheets, Tree.probabilityNodeToleafPer1SinglesSheets, Tree.probabilityNodeToleafPer2SinglesSheets, Tree.probabilityNodeToleafPer3SinglesSheets));

                }

            }
            else
            {
                Tree.probabilityNodeToleafPer0SinglesSheets = savedNode.probabilityNodeToleafPer0SinglesSheets;
                Tree.probabilityNodeToleafPer1SinglesSheets = savedNode.probabilityNodeToleafPer1SinglesSheets;
                Tree.probabilityNodeToleafPer2SinglesSheets = savedNode.probabilityNodeToleafPer2SinglesSheets;
                Tree.probabilityNodeToleafPer3SinglesSheets = savedNode.probabilityNodeToleafPer3SinglesSheets;

                var probabilidadEn0 = probability;
                foreach (var element in savedNode.probabilityNodeToleafPer0SinglesSheets)
                {
                    TotalProbabilityPerSinglesSheets[0] += probabilidadEn0 * element;
                }

                var probabilidadEn1 = probability;
                foreach (var element in savedNode.probabilityNodeToleafPer1SinglesSheets)
                {
                    TotalProbabilityPerSinglesSheets[1] += probabilidadEn1 * element;
                }
                var probabilidadEn2 = probability;
                foreach (var element in savedNode.probabilityNodeToleafPer2SinglesSheets)
                {
                    TotalProbabilityPerSinglesSheets[2] += probabilidadEn2 * element;
                }
                var probabilidadEn3 = probability;
                foreach (var element in savedNode.probabilityNodeToleafPer3SinglesSheets)
                {
                    TotalProbabilityPerSinglesSheets[3] += probabilidadEn3 * element;
                }
            }

            return found;
        }
    }
}
