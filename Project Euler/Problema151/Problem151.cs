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

            for (var i = 0; i <= 3; i++)
            {
                var probabilityAux = 0.0;
                foreach (var element in Tree.probabilityNodeToleafPerSinglesSheets[i])
                {

                    probabilityAux += element;
                }
                TotalProbabilityPerSinglesSheets[i] = probabilityAux;
            }

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

                if (!Tree.IsLeaf)
                {
                    var nodeProbability = (1.0 / double.Parse(Tree.GetNode().Count.ToString()));
                    probability = probability * nodeProbability;

                    for (var i = Tree.GetNode().Count - 1; i >= 0; i--)
                    {
                        var newTree = new NTree<List<int>>(Tree.GenerateNewNode(i));

                        CalculateNextNode(NumSingleSheets, ref newTree, probability);

                        var k = 0;
                        if (Tree.IsSolitary)
                        {
                            k = 1;
                        }

                        for (var j = 0; j <= 2; j++)
                        {
                            foreach (var element in newTree.probabilityNodeToleafPerSinglesSheets[j])
                            {
                                Tree.probabilityNodeToleafPerSinglesSheets[j + k].Add(nodeProbability * element);                             
                            }
                        }                             
                    }

                    memory.Add(new MemoriaParaP151(Tree.GetNode(), Tree.probabilityNodeToleafPerSinglesSheets));
                }
                else
                {
                    Tree.probabilityNodeToleafPerSinglesSheets[0].Add(1.0);

                    memory.Add(new MemoriaParaP151(Tree.GetNode(), Tree.probabilityNodeToleafPerSinglesSheets));
                }
            }
            else
            {
                Tree.probabilityNodeToleafPerSinglesSheets = savedNode.probabilityNodeToleafPerSinglesSheets;
            }

            return found;
        }
    }
}
