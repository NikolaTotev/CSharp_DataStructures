using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomStructures
{
    public class WeightedUndirectedGraphNode<T,W>
    {
        private readonly Dictionary<T, WeightedGraphConnection<T,W>> m_Neighbors;
        public T Data { get; set; }
        public bool IsVisited { get; set; }

        public WeightedUndirectedGraphNode(T data)
        {

            m_Neighbors = new Dictionary<T,WeightedGraphConnection<T,W>>();
            Data = data;
        }

        public IReadOnlyDictionary<T,WeightedGraphConnection<T,W>> GetNeighbors()
        {
            return m_Neighbors;
        }

        public WeightedUndirectedGraphNode(WeightedUndirectedGraphNode<T,W> initialNeighbor, T data, W connectionWeight) : this(data)
        {
            initialNeighbor.AddNeighbor(this,connectionWeight);
        }

        public void AddNeighbor(WeightedUndirectedGraphNode<T,W> neighborToAdd, W connectionWeight)
        {
            if (!m_Neighbors.ContainsKey(neighborToAdd.Data))
            {
                WeightedGraphConnection<T,W> newConnection = new WeightedGraphConnection<T, W>(neighborToAdd, connectionWeight);
                m_Neighbors.Add(neighborToAdd.Data, newConnection);
                neighborToAdd.AddNeighbor(this,connectionWeight);
            }
        }

        public void Print()
        {
            Console.Write("{0} ", Data);
        }
    }
}
