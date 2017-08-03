using System.Collections.Generic;

namespace Project_Euler.Utils
{
    public delegate void TreeVisitor<T>(T nodeData);

    public class NTree<T>
    {
        private T data;
        private LinkedList<NTree<T>> children;

        public NTree(T data)
        {
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
                if (--i == 0)
                    return n;
            return null;
        }

        public void Traverse(NTree<T> node, TreeVisitor<T> visitor)
        {
            visitor(node.data);
            foreach (NTree<T> kid in node.children)
                Traverse(kid, visitor);
        }

        public int TraverseTakenProbabilities(NTree<T> node, TreeVisitor<T> visitor)
        {
            var probabilityInLevel = 1 / node.children.Count;
            
            foreach (NTree<T> kid in node.children)
            {
                var probabilityLevelMinus1 = TraverseTakenProbabilities(kid, visitor);
                probabilityInLevel = probabilityInLevel * probabilityLevelMinus1;
            }

            return probabilityInLevel;
        }
    }
}
