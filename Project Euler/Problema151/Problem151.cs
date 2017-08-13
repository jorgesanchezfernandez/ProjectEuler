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

            var NumTimes = 0;

            CalculateNextNode(NumTimes, ref Tree);

            var totalLeafs = 0;
            for (var i = 0; i <= NumTotalOfBranchesPerTimes.Length - 1; i++)
            {
                totalLeafs += NumTotalOfBranchesPerTimes[i];
            }

            decimal probabilityPerOneTime = Decimal.Parse(NumTotalOfBranchesPerTimes[1].ToString()) / totalLeafs;
            decimal probabilityPerOneTwo = Decimal.Parse(NumTotalOfBranchesPerTimes[2].ToString()) / totalLeafs;
            decimal probabilityPerOneThree = Decimal.Parse(NumTotalOfBranchesPerTimes[3].ToString()) / totalLeafs;

            //decimal probabilityPerOneTime = Decimal.Parse(NumTotalOfBranchesPerTimes[1].ToString()) / NumTotalTreeLeafs;
            //decimal probabilityPerOneTwo = Decimal.Parse(NumTotalOfBranchesPerTimes[2].ToString()) / NumTotalTreeLeafs;
            //decimal probabilityPerOneThree = Decimal.Parse(NumTotalOfBranchesPerTimes[3].ToString()) / NumTotalTreeLeafs;

            var solution = probabilityPerOneTime + 2 * probabilityPerOneTwo + 3 * probabilityPerOneThree;

            // Para el contador e imprime el resultado:
            DateTime tiempo2 = DateTime.Now;
            TimeSpan total = new TimeSpan(tiempo2.Ticks - tiempo1.Ticks);
            Console.WriteLine("TIEMPO: " + total.TotalSeconds);

            Console.WriteLine("TOTAL NUMBER OF BRANCHES: " + totalLeafs);

            //Console.WriteLine("TOTAL NUMBER OF BRANCHES: " + NumTotalTreeLeafs);

            Console.WriteLine("EXPECTED NUMBER: " + solution);

            Console.ReadKey();
        }

        public static bool CalculateNextNode (int NumTimes, ref NTree<List<int>> Tree)
        {
            var savedNode = memory.Find(Tree.GetNode());
            var found = savedNode != null;

            if (!found)
            {
                if (Tree.IsSolitary)
                    NumTimes++;

                if (Tree.IsLeaf)
                {
                    if (Tree.IsSolitary)
                    {
                        Tree.NumTotalOfBranchesPerTimes[1]++;
                        Tree.NumTotalNodeLeafs++;

                        NumTotalOfBranchesPerTimes[NumTimes]++;
                        NumTotalTreeLeafs++;
                    }
                    else
                    {
                        Tree.NumTotalOfBranchesPerTimes[0] += 2;
                        Tree.NumTotalNodeLeafs += 2;

                        NumTotalOfBranchesPerTimes[NumTimes] += 2;
                        NumTotalTreeLeafs += 2;
                    }
                }
                else
                {
                    for (var i = Tree.GetNode().Count - 1; i >= 0; i--)
                    {
                        var newTree = new NTree<List<int>>(Tree.GenerateNewNode(i));
                        if (!CalculateNextNode(NumTimes, ref newTree))
                        {                       
                            var nextNodeBranchesTimes = newTree.NumTotalOfBranchesPerTimes;
                            var totalNodeLeafs = newTree.NumTotalNodeLeafs;
                            if (Tree.IsSolitary && !Tree.IsLeaf)
                            {
                                Tree.NumTotalOfBranchesPerTimes[0] = 0;
                                for (var j = 1; j <= newTree.NumTotalOfBranchesPerTimes.Length - 1; j++)
                                {
                                    Tree.NumTotalOfBranchesPerTimes[j] += nextNodeBranchesTimes[j - 1];
                                }
                            }                         
                            else{
                                for (var j = 0; j <= newTree.NumTotalOfBranchesPerTimes.Length - 1; j++)
                                {
                                    Tree.NumTotalOfBranchesPerTimes[j] += nextNodeBranchesTimes[j];
                                }
                            }
                            Tree.NumTotalNodeLeafs += newTree.NumTotalNodeLeafs;
                            
                            //Si sale el 2 ya podemos salir y si sale el 3 en la primera rama podemos salir
                            memory.Add(new MemoriaParaP151(newTree.GetNode(), newTree.NumTotalOfBranchesPerTimes, newTree.NumTotalNodeLeafs));
                        }
                    }
                }
            }
            else
            {
                //Rrevisar que este haciendo bien esto
                var nextNodeBranchesTimes = savedNode.guardaNumTotalDeRamasPorSolitarios;
                var totalNodeLeafs = savedNode.numeroDeHojasDelNodo;

                for (var j = NumTimes; j <= NumTotalOfBranchesPerTimes.Length - 1; j++)
                {
                    NumTotalOfBranchesPerTimes[j] += nextNodeBranchesTimes[j - NumTimes];
                }
                NumTotalTreeLeafs += totalNodeLeafs;
            }

            return found;
        }
    }
}
