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
        private readonly Dictionary<T, UndirectedGraphNode<T>> m_Neighbors;
        public T Data { get; set; }
        public bool IsVisited { get; set; }

        public UndirectedGraphNode(T data)
        {

            m_Neighbors = new Dictionary<T, UndirectedGraphNode<T>>();
            Data = data;
        }

        public IReadOnlyDictionary<T, UndirectedGraphNode<T>> GetNeighbors()
        {
            return m_Neighbors;
        }

        public UndirectedGraphNode(UndirectedGraphNode<T> initialNeighbor, T data) : this(data)
        {
            initialNeighbor.m_Neighbors.Add(data, this);
        }

        public void AddNeighbor(UndirectedGraphNode<T> neighborToAdd)
        {
            if (!m_Neighbors.ContainsKey(neighborToAdd.Data))
            {
                m_Neighbors.Add(neighborToAdd.Data, neighborToAdd);
                neighborToAdd.AddNeighbor(this);
            }
        }

        public void Print()
        {
            Console.Write("{0} ", Data);
        }
    }
}
