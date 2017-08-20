using System.Collections.Generic;

namespace Project_Euler.Utils
{
    using System;
    using System.Linq;

    public class NTree<T>
    {
        private List<int> data;

        private bool Leaf;

        public List<List<double>> probabilityNodeToleafPerSinglesSheets { get; set; }

        public NTree(List<int> data)
        {
            this.data = data;

            this.probabilityNodeToleafPerSinglesSheets = new List<List<double>>();
            this.probabilityNodeToleafPerSinglesSheets.Add(new List<double>());
            this.probabilityNodeToleafPerSinglesSheets.Add(new List<double>());
            this.probabilityNodeToleafPerSinglesSheets.Add(new List<double>());
            this.probabilityNodeToleafPerSinglesSheets.Add(new List<double>());

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
