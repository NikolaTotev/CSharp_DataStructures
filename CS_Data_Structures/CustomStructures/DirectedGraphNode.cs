using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CustomStructures
{
    public class DirectedGraphNode<T>
    {
        private readonly List<DirectedGraphNode<T>> m_Neighbors;
        public T Data { get; set; }

        public DirectedGraphNode(T data)
        {
            m_Neighbors = new List<DirectedGraphNode<T>>();
            Data = data;
        }

        public DirectedGraphNode(DirectedGraphNode<T> initialNeighbor, T data) : this(data)
        {
           m_Neighbors.Add(initialNeighbor);
        }


        public IReadOnlyCollection<DirectedGraphNode<T>> GetNeighbors()
        {
            return m_Neighbors.AsReadOnly();
        }

        public void AddNeighbor(DirectedGraphNode<T> neighborToAdd)
        {
            if (!m_Neighbors.Contains(neighborToAdd))
            {
                m_Neighbors.Add(neighborToAdd);
            }
        }

        public void RemoveNeighbor(DirectedGraphNode<T> neighborToRemove)
        {
            if (m_Neighbors.Contains(neighborToRemove))
            {
                m_Neighbors.Remove(neighborToRemove);
            }
        }

        public void Print()
        {
            Console.Write("{0} ", Data);
        }

    }
}
