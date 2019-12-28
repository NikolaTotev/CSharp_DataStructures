using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CustomStructures
{
    public class UndirectedGraphNode<T>
    {
        private readonly List<UndirectedGraphNode<T>> m_Neighbors;
        public T Data { get; set; }
        public UndirectedGraphNode(T data)
        {

            m_Neighbors = new List<UndirectedGraphNode<T>>();
            Data = data;
        }

        public IReadOnlyCollection<UndirectedGraphNode<T>> GetNeighbors()
        {
            return m_Neighbors.AsReadOnly();
        }

        public UndirectedGraphNode(UndirectedGraphNode<T> initialNeighbor, T data) : this(data)
        {
            initialNeighbor.m_Neighbors.Add(this);
        }


        //public UndirectedGraphNode(UndirectedGraphNode<T> source):this(source.Data)
        //{
        //    foreach (var undirectedGraphNode in source.GetNeighbors())
        //    {
        //        UndirectedGraphNode<T> newCopy = new UndirectedGraphNode<T>(undirectedGraphNode); //Makes deep copy of the neighbor list only
        //        AddNeighbor(newCopy);
        //    }
        //}

        public void AddNeighbor(UndirectedGraphNode<T> neighborToAdd)
        {
            if (!m_Neighbors.Contains(neighborToAdd))
            {
                m_Neighbors.Add(neighborToAdd);
                neighborToAdd.AddNeighbor(this);
            }
        }

        public void RemoveNeighbor(UndirectedGraphNode<T> neighborToRemove)
        {
            if (m_Neighbors.Contains(neighborToRemove))
            {
                m_Neighbors.Remove(neighborToRemove);
                neighborToRemove.RemoveNeighbor(this);
            }
        }

        public void Print()
        {
            Console.Write("{0} ", Data);
        }
    }
}
