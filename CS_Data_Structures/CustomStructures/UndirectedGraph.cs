using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CustomStructures
{
    public class UndirectedGraph<T>
    {
        public Dictionary<T, UndirectedGraphNode<T>> Nodes { get; set; }
        public int NumberOfComponents { get; set; }

        public UndirectedGraph(T initialNodeData)
        {
            Nodes = new Dictionary<T, UndirectedGraphNode<T>>();
            UndirectedGraphNode<T> initialNode = new UndirectedGraphNode<T>(initialNodeData);
            Nodes.Add(initialNodeData, initialNode);
            NumberOfComponents = 0;
        }

        public void AddEdge(T vertexOne, T vertexTwo)
        {
            if (vertexOne == null) throw new ArgumentNullException(nameof(vertexOne));
            if (vertexTwo == null) throw new ArgumentNullException(nameof(vertexTwo));

            bool v2Index = Nodes.ContainsKey(vertexTwo);
            bool v1Index = Nodes.ContainsKey(vertexOne);

            if (!v1Index)
            {
                UndirectedGraphNode<T> nodeOne = new UndirectedGraphNode<T>(vertexOne);
                Nodes.Add(vertexOne, nodeOne);
            }

            if (!v2Index)
            {

                UndirectedGraphNode<T> nodeTwo = new UndirectedGraphNode<T>(vertexTwo);
                Nodes.Add(vertexTwo, nodeTwo);
            }


            Nodes[vertexTwo].AddNeighbor(Nodes[vertexOne]);
        }

        public void AddVertex(T vertex)
        {
            bool v2Index = Nodes.ContainsKey(vertex);

            if (!v2Index)
            {
                UndirectedGraphNode<T> newNode = new UndirectedGraphNode<T>(vertex);
                Nodes.Add(vertex, newNode);
            }
        }

        public void RemoveEdge(T vertexOne, T vertexTwo)
        {
            bool v2Index = Nodes.ContainsKey(vertexTwo);
            bool v1Index = Nodes.ContainsKey(vertexOne);

            if (v1Index && v2Index)
            {
                Nodes[vertexTwo].RemoveNeighbor(Nodes[vertexOne]);
            }

        }

        public void RemoveEdgeAndVertex(T vertexOne, T vertexTwo)
        {
            bool v2Index = Nodes.ContainsKey(vertexTwo);
            bool v1Index = Nodes.ContainsKey(vertexOne);

            if (v1Index)
            {
                Nodes.Remove(vertexOne);
            }

            if (v2Index)
            {
                Nodes.Remove(vertexTwo);
            }

            Nodes[vertexTwo].RemoveNeighbor(Nodes[vertexOne]);
        }


        /// <summary>
        /// Generic DFS function. Can be used to solve any problem that requires DFS, just plug in the inspectionFunc.
        /// </summary>
        /// <param name="inspectFunc">Must return true if the search is completed and no more nodes need to be checked</param>
        public void Dfs(Func<UndirectedGraphNode<T>, bool> inspectFunc)
        {
            List<UndirectedGraphNode<T>> nodes = Nodes.Values.ToList();
            if (nodes.Count == 0)
            {
                return;
            }
            bool[] visited = new bool[nodes.Count];
            visited.Initialize();

            foreach (var node in nodes)
            {
                if (!visited[nodes.IndexOf(node)])
                {
                    InternalDfs(nodes, visited, node, inspectFunc);
                }
            }
        }
        private void InternalDfs(List<UndirectedGraphNode<T>> nodes, bool[] visited, UndirectedGraphNode<T> node, Func<UndirectedGraphNode<T>, bool> inspectFunc)
        {
            int nodeId = nodes.IndexOf(node);
            visited[nodeId] = true;

            if (inspectFunc(node))
            {
                return;
            }
            foreach (UndirectedGraphNode<T> neighbor in node.GetNeighbors())
            {
                if (!visited[nodes.IndexOf(neighbor)])
                {
                    InternalDfs(nodes, visited, neighbor, inspectFunc);
                }
            }
        }

        /// <summary>
        /// Generic BFS function. Can be used to solve any problem that requires BFS, just plug in the inspectionFunc.
        /// </summary>
        /// <param name="inspectFunc">Must return true if the search is completed and no more nodes need to be checked</param>
        /// <param name="startId">Starting node, by default it is 0.</param>
        public void Bfs(Func<UndirectedGraphNode<T>, bool> inspectFunc, bool countComponents)
        {
            List<UndirectedGraphNode<T>> nodes = Nodes.Values.ToList();
            if (nodes.Count == 0)
            {
                return;
            }

            bool[] visited = new bool[nodes.Count];
            visited.Initialize();
            Queue<UndirectedGraphNode<T>> nodeQueue = new Queue<UndirectedGraphNode<T>>();
            foreach (var undirectedGraphNode in nodes)
            {

                int currentNodeId = nodes.IndexOf(undirectedGraphNode);
                if (!visited[currentNodeId])
                {
                    if (countComponents)
                    {
                        NumberOfComponents++;
                    }
                    nodeQueue.Enqueue(undirectedGraphNode);
                    visited[currentNodeId] = true;
                    while (nodeQueue.Count != 0)
                    {
                        UndirectedGraphNode<T> currentNode = nodeQueue.Dequeue();

                        if (inspectFunc != null && inspectFunc(currentNode))
                        {
                            return;
                        }

                        foreach (UndirectedGraphNode<T> neighbor in currentNode.GetNeighbors())
                        {
                            if (!visited[nodes.IndexOf(neighbor)])
                            {
                                visited[nodes.IndexOf(neighbor)] = true;
                                nodeQueue.Enqueue(neighbor);
                            }
                        }

                    }
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
