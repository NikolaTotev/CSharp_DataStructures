using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomStructures
{
    public class WeightedUndirectedGraph<T>
    {
        private readonly int m_InfinityWeightValue;
        private readonly int m_ZeroWeightValue;
        public Dictionary<T, WeightedUndirectedGraphNode<T, int>> Nodes { get; set; }
        public int NumberOfComponents { get; set; }


        public WeightedUndirectedGraph(T initialNodeData)
        {

            Nodes = new Dictionary<T, WeightedUndirectedGraphNode<T, int>>();
            WeightedUndirectedGraphNode<T, int> initialNode = new WeightedUndirectedGraphNode<T, int>(initialNodeData);
            Nodes.Add(initialNodeData, initialNode);
            NumberOfComponents = 0;
            m_InfinityWeightValue = int.MaxValue;
            m_ZeroWeightValue = 0;
        }

        public void AddEdge(T vertexOne, T vertexTwo, int connectionWeight)
        {
            if (vertexOne == null) throw new ArgumentNullException(nameof(vertexOne));
            if (vertexTwo == null) throw new ArgumentNullException(nameof(vertexTwo));

            bool v2Index = Nodes.ContainsKey(vertexTwo);
            bool v1Index = Nodes.ContainsKey(vertexOne);

            if (!v1Index)
            {
                WeightedUndirectedGraphNode<T, int> nodeOne = new WeightedUndirectedGraphNode<T, int>(vertexOne);
                Nodes.Add(vertexOne, nodeOne);
            }

            if (!v2Index)
            {

                WeightedUndirectedGraphNode<T, int> nodeTwo = new WeightedUndirectedGraphNode<T, int>(vertexTwo);
                Nodes.Add(vertexTwo, nodeTwo);
            }


            Nodes[vertexTwo].AddNeighbor(Nodes[vertexOne], connectionWeight);
        }

        public void AddVertex(T vertex)
        {
            bool v2Index = Nodes.ContainsKey(vertex);

            if (!v2Index)
            {
                WeightedUndirectedGraphNode<T, int> newNode = new WeightedUndirectedGraphNode<T, int>(vertex);
                Nodes.Add(vertex, newNode);
            }
        }

        /// <summary>
        /// Generic DFS function. Can be used to solve any problem that requires DFS, just plug in the inspectionFunc.
        /// </summary>
        /// <param name="inspectFunc">Must return true if the search is completed and no more nodes need to be checked</param>
        public void Dfs(Func<WeightedUndirectedGraphNode<T, int>, bool> inspectFunc)
        {
            if (Nodes.Count == 0)
            {
                return;
            }

            foreach (var node in Nodes)
            {
                if (node.Value.IsVisited)
                {
                    InternalDfs(Nodes, node.Value, inspectFunc);
                }
            }
        }
        private void InternalDfs(Dictionary<T, WeightedUndirectedGraphNode<T, int>> nodes, WeightedUndirectedGraphNode<T, int> node, Func<WeightedUndirectedGraphNode<T, int>, bool> inspectFunc)
        {
            if (inspectFunc(node))
            {
                return;
            }
            foreach (var neighbor in node.GetNeighbors())
            {
                if (!neighbor.Value.ConnectionNeighbor.IsVisited)
                {
                    InternalDfs(nodes, neighbor.Value.ConnectionNeighbor, inspectFunc);
                }
            }
        }

        /// <summary>
        /// Generic BFS function. Can be used to solve any problem that requires BFS, just plug in the inspectionFunc.
        /// </summary>
        /// <param name="inspectFunc">Must return true if the search is completed and no more nodes need to be checked</param>
        /// <param name="countComponents">Bool that when enabled counts the number of components in a graph.</param>
        public void Bfs(Func<WeightedUndirectedGraphNode<T, int>, bool> inspectFunc, bool countComponents)
        {
            if (Nodes.Count == 0)
            {
                return;
            }

            Queue<WeightedUndirectedGraphNode<T, int>> nodeQueue = new Queue<WeightedUndirectedGraphNode<T, int>>();
            foreach (var undirectedGraphNode in Nodes)
            {

                if (!undirectedGraphNode.Value.IsVisited)
                {
                    if (countComponents)
                    {
                        NumberOfComponents++;
                    }
                    nodeQueue.Enqueue(undirectedGraphNode.Value);
                    undirectedGraphNode.Value.IsVisited = true;
                    while (nodeQueue.Count != 0)
                    {
                        WeightedUndirectedGraphNode<T, int> currentNode = nodeQueue.Dequeue();

                        if (inspectFunc != null && inspectFunc(currentNode))
                        {
                            return;
                        }

                        foreach (var neighbor in currentNode.GetNeighbors())
                        {
                            if (!neighbor.Value.ConnectionNeighbor.IsVisited)
                            {
                                neighbor.Value.ConnectionNeighbor.IsVisited = true;
                                nodeQueue.Enqueue(neighbor.Value.ConnectionNeighbor);
                            }
                        }
                    }
                }
            }
        }

        public void FindPath(T source)
        {
            WeightedUndirectedGraphNode<T, int> sourceNode = Nodes[source];
            SortedDictionary<WeightedUndirectedGraphNode<T, int>, int> distances = new SortedDictionary<WeightedUndirectedGraphNode<T, int>, int>();
            distances[sourceNode] = m_ZeroWeightValue;
            foreach (var node in Nodes)
            {
                distances[node.Value] = m_InfinityWeightValue;
            }

            while (distances.Count > 0)
            {
                WeightedUndirectedGraphNode<T, int> currentVertex = Nodes[distances.Min().Key.Data];
                distances.Remove(currentVertex);

                foreach (var neighbor in currentVertex.GetNeighbors())
                {
                    int alternativeDist = distances[neighbor.Value.ConnectionNeighbor] + neighbor.Value.ConnectionWeight;
                }
            }

        }

        public int CountComponents()
        {
            Bfs(null, true);
            return NumberOfComponents;
        }
    }
}
