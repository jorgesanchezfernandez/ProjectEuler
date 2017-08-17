using System.Collections.Generic;

namespace Project_Euler.Utils
{
    using System;
    using System.Linq;

    public class NTree<T>
    {
        private List<int> data;

        private bool Leaf;

        public List<double> probabilityNodeToleafPer0SinglesSheets { get; set; }
        public List<double> probabilityNodeToleafPer1SinglesSheets { get; set; }
        public List<double> probabilityNodeToleafPer2SinglesSheets { get; set; }
        public List<double> probabilityNodeToleafPer3SinglesSheets { get; set; }


        public NTree(List<int> data)
        {
            this.data = data;

            this.probabilityNodeToleafPer0SinglesSheets = new List<double>();
            this.probabilityNodeToleafPer1SinglesSheets = new List<double>();
            this.probabilityNodeToleafPer2SinglesSheets = new List<double>();
            this.probabilityNodeToleafPer3SinglesSheets = new List<double>();

            Leaf = data.Count == 1 && data[0] == 5;
        }

        public List<int> GetNode()
        {
            return data;
        }

        public bool IsLeaf
        {
            get
            {
                return Leaf;
            }
        }

        public bool IsSolitary
        {
            get
            {
                return data.Count == 1 && data[0] != 5;
            }
        }

        public List<int> GenerateNewNode(int i)
        {
            var nuevoNodo = new List<int>();
            for (var j = 0; j < data.Count; j++)
            {
                if (j != i)
                {
                    nuevoNodo.Add(data[j]);
                }
            }

            if (data[i] == 2)
            {
                nuevoNodo.Add(3);
                nuevoNodo.Add(4);
                nuevoNodo.Add(5);
            }
            if (data[i] == 3)
            {
                nuevoNodo.Add(4);
                nuevoNodo.Add(5);
            }
            if (data[i] == 4)
            {
                nuevoNodo.Add(5);
            }
            nuevoNodo.Sort(); // Importante para no repetir las busquedas
            return nuevoNodo;
        }
    }
}
