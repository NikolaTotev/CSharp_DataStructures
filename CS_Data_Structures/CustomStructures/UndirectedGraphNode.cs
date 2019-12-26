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
        public List<UndirectedGraphNode<T>> Neighbors { get; set; }
        public T Data { get; set; }

        public UndirectedGraphNode(T data)
        {
            Neighbors = new List<UndirectedGraphNode<T>>();
            Data = data;
        }

        public UndirectedGraphNode(UndirectedGraphNode<T> initialNeighbor, T data)
        {
            Neighbors = new List<UndirectedGraphNode<T>>();
            Neighbors.Add(initialNeighbor);
            initialNeighbor.Neighbors.Add(this);
            Data = data;
        }

        public void AddNeighbor(UndirectedGraphNode<T> neighborToAdd)
        {
            Neighbors.Add(neighborToAdd);
        }

        public void RemoveNeighbor(UndirectedGraphNode<T> neighborToRemove)
        {
            Neighbors.Remove(neighborToRemove);
        }
    }
}
