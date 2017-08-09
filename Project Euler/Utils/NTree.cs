using System.Collections.Generic;

namespace Project_Euler.Utils
{
    public class NTree<T>
    {
        private List<int> data;
        private LinkedList<NTree<List<int>>> children;
        private int totalLonelyNumbersInNode;
        
        public NTree(List<int> data)
        {
            this.data = data;
            for (var j = data.Count - 1; j >= 0; j--)
            {
                AddChild(CrearNuevoNodo(data, j));
            }
        }

        public void AddChild(List<int> data)
        {
            children.AddFirst(new NTree<List<int>>(data));
        }

        public NTree<List<int>> GetChild(int i)
        {
            foreach (NTree<List<int>> n in children)
                if (--i == 0)
                    return n;
            return null;
        }

        public LinkedList<NTree<List<int>>> GetChildren()
        {
            return children;
        }

        public List<int> GetNode()
        {
            return data;
        }

        public bool IsLeaf
        {
            get
            {
                return children.Count == 0;
            }
        }

        private List<int> CrearNuevoNodo(List<int> data, int i)
        {
            var nuevoNodo = new List<int>();
            for (var j = 0; j < data.Count; j++)
            {
                if (j != i)
                {
                    nuevoNodo.Add(data[j]);
                }
            }
            if ((nuevoNodo.Count == 0) && (data[i] != 5))
            {
                totalLonelyNumbersInNode++;
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

        //public void Traverse(NTree<T> node)
        //{
        //    if (!IsRepeated())
        //    {
        //        if(node.IsLeaf)
        //        {
        //            SetValues();
        //        }else
        //        {
        //            foreach (NTree<T> kid in node.children)
        //            {
        //                Traverse(kid);
        //            }
        //        } 
        //    }else
        //    {
        //        GetValues();
        //    }
        //}
    }
}
