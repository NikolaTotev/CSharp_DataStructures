using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomStructures
{
    public class UndirectedGraph<T>
    {
        public List<UndirectedGraphNode<T>> AdjecencyList { get; set; }

        public UndirectedGraph(T initialNodeData)
        {
            UndirectedGraphNode<T> initialNode = new UndirectedGraphNode<T>(initialNodeData);
            AdjecencyList.Add(initialNode);
        }

        public UndirectedGraph(UndirectedGraphNode<T> initialNode)
        {
            AdjecencyList.Add(initialNode);
        }

        public void AddEdge(UndirectedGraphNode<T> vertexOne, UndirectedGraphNode<T> vertexTwo)
        {
            int v1Index = -1;
            int v2Index = -1;

            if (AdjecencyList.Contains(vertexOne))
            {
                v1Index = AdjecencyList.IndexOf(vertexOne);
            }

            if (AdjecencyList.Contains(vertexTwo))
            {
                v2Index = AdjecencyList.IndexOf(vertexTwo);
            }

            if (v1Index == -1 && v2Index != -1)
            {
                AdjecencyList.Add(vertexOne);
                vertexTwo.AddNeighbor(vertexOne);
                return;
            }

            if (v2Index == -1 && v1Index != -1)
            {
                AdjecencyList.Add(vertexTwo);
                vertexOne.AddNeighbor(vertexTwo);
                return;
            }

            if (v1Index == -1 && v2Index == -1)
            {
                AdjecencyList.Add(vertexOne);
                AdjecencyList.Add(vertexTwo);

                vertexOne.AddNeighbor(vertexTwo);
                vertexTwo.AddNeighbor(vertexOne);
                return;
            }

            if (v1Index != -1 && v2Index != -1)
            {
                if (!vertexOne.Neighbors.Contains(vertexTwo))
                {
                    vertexOne.AddNeighbor(vertexTwo);
                }
                if (!vertexTwo.Neighbors.Contains(vertexOne))
                {
                    vertexTwo.AddNeighbor(vertexOne);
                }
            }
        }
    }
}
