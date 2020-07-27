using System;
using System.Collections.Generic;
using System.Text;

namespace Navigation_solution_
{
    public class Node<T>
    {
        public T Data;

        public List<Node<T>> Children { get; set; }

        public Node()
        {
            Children = new List<Node<T>>();
        }


        public List<Node<T>> LevelOrder()
        {
            List<Node<T>> list = new List<Node<T>>();
            Queue<Node<T>> queue = new Queue<Node<T>>();
            queue.Enqueue(this);
            while (queue.Count != 0)
            {
                Node<T> temp = queue.Dequeue();
                foreach (Node<T> child in temp.Children)
                    queue.Enqueue(child);
                list.Add(temp);
            }
            return list;
        }

        public List<Node<T>> PreOrder()
        {
            List<Node<T>> list = new List<Node<T>>();
            list.Add(this);
            foreach (Node<T> child in Children)
                list.AddRange(child.PreOrder());
            return list;
        }

        public List<Node<T>> PostOrder()
        {
            List<Node<T>> list = new List<Node<T>>();
            foreach (Node<T> child in Children)
                list.AddRange(child.PreOrder());
            list.Add(this);
            return list;
        }
    }
}
