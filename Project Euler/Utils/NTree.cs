﻿using System.Collections.Generic;

namespace Project_Euler.Utils
{
    using System;
    using System.Linq;

    public class NTree<T>
    {
        private List<int> data;

        private LinkedList<NTree<List<int>>> children;

        private bool Leaf;

        public List<List<double>> probabilityNodeToleafPerSinglesSheets { get; set; }

        public NTree(List<int> data)
        {
            this.data = data;

            children = new LinkedList<NTree<List<int>>>();

            this.probabilityNodeToleafPerSinglesSheets = new List<List<double>>();

            Leaf = data.Count == 1 && data[0] == 5;
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

        public int NumOfChildren()
        {
             return children.Count;
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
