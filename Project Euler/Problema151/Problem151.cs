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

        public static List<MemoriaParaP151> memory = new List<MemoriaParaP151>();

 
        static void Main(string[] args)
        {
            // Inicia el contador:
            DateTime tiempo1 = DateTime.Now;
            
            var NumTimes = 0;
            ulong NumTotalNodeLeafs = 0;

            if (true/*!IsRepeated()*/)
            {
                if (Tree.IsSolitary)
                    NumTimes++;

                if (Tree.IsLeaf)
                {
                    Tree.NumTotalOfBranchesPerTimes[NumTimes]++;
                    Tree.NumTotalNodeLeafs = NumTotalNodeLeafs++;
                    memory.Add(new MemoriaParaP151(Tree.GetNode(), Tree.NumTotalOfBranchesPerTimes, Tree.NumTotalNodeLeafs));

                    NumTotalOfBranchesPerTimes[NumTimes]++;
                    NumTotalTreeLeafs++;
                }
                else
                {
                    for (var i = Tree.GetNode().Count - 1; i >= 0; i--)
                    {
                        var newTree = new NTree<List<int>>(Tree.GenerateNewNode(i));
                        // CalculateNextNode(....);
                        //Save in memory all data from new Tree
                        memory.Add(new MemoriaParaP151(newTree.GetNode(), newTree.NumTotalOfBranchesPerTimes, newTree.NumTotalNodeLeafs));
                    }
                }
            }
            else
            {
                //GetValues();
                //Add values to the total
            }

            //Number expected
            decimal probabilityPerOneTime = Decimal.Parse(NumTotalOfBranchesPerTimes[1].ToString()) / NumTotalTreeLeafs;
            decimal probabilityPerOneTwo = Decimal.Parse(NumTotalOfBranchesPerTimes[2].ToString()) / NumTotalTreeLeafs;
            decimal probabilityPerOneThree = Decimal.Parse(NumTotalOfBranchesPerTimes[3].ToString()) / NumTotalTreeLeafs;

            var solution = probabilityPerOneTime + 2 * probabilityPerOneTwo + 3 * probabilityPerOneThree;

            // Para el contador e imprime el resultado:
            DateTime tiempo2 = DateTime.Now;
            TimeSpan total = new TimeSpan(tiempo2.Ticks - tiempo1.Ticks);
            Console.WriteLine("TIEMPO: " + total.TotalSeconds);
            
            Console.WriteLine("TOTAL NUMBER OF BRANCHES: ");

            Console.WriteLine("EXPECTED NUMBER: " );

            Console.ReadKey();
        }

        //private static int BuscarNodoEnLista(NTree<List<int>> arbol)
        //{
        //    var indice = -1;
        //    var encontrado = false;

        //    for (var i = memoria.Count - 1; i > -1 && memoria[i].nodo.Count >= arbol.GetNode().Count && i >= Math.Ceiling(memoria.Count / CeilingDividor) && !encontrado; i--)
        //    {
        //        if (memoria[i].nodo.Count == arbol.GetNode().Count)
        //        {
        //            var iguales = true;
        //            for (var j = 0; j <= memoria[i].nodo.Count - 1 && iguales; j++)
        //            {
        //                iguales = memoria[i].nodo[j] == arbol.GetNode()[j];
        //            }
        //            encontrado = iguales;
        //            indice = encontrado == true ? i : -1;
        //        }
        //    }

        //    return indice;
        //}
    }
}
