using System.Collections.Generic;

namespace Project_Euler.Utils
{
    public class NTree<T>
    {
        private bool visited;
        private T data;
        private LinkedList<NTree<T>> children;
        
        public NTree(T data)
        {
            this.visited = false;
            this.data = data;
            children = new LinkedList<NTree<T>>();
        }

        public void AddChild(T data)
        {
            children.AddFirst(new NTree<T>(data));
        }

        public NTree<T> GetChild(int i)
        {
            foreach (NTree<T> n in children)
            {
                if (--i == 0)
                {
                    visited = true;
                    return n;
                }
            }
            return null;
        }

        public LinkedList<NTree<T>> GetChildren()
        {
            return children;
        }

        public T GetNode()
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
