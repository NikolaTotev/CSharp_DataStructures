using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomStructures
{
    public class DirectedGraph<T>
    {
        public Dictionary<T, DirectedGraphNode<T>> Nodes { get; set; }
        public int NumberOfComponents { get; set; }

        public DirectedGraph(T initialNodeData)
        {
            Nodes = new Dictionary<T, DirectedGraphNode<T>>();
            DirectedGraphNode<T> initialNode = new DirectedGraphNode<T>(initialNodeData);
            Nodes.Add(initialNodeData, initialNode);
            NumberOfComponents = 0;
        }

        /// <summary>
        /// Adds an edge to the directed graph. The direction is from the head vertex to the tail vertex.
        /// </summary>
        /// <param name="headVertex"></param>
        /// <param name="tailVertex"></param>
        public void AddEdge(T headVertex, T tailVertex)
        {
            if (headVertex == null) throw new ArgumentNullException(nameof(headVertex));
            if (tailVertex == null) throw new ArgumentNullException(nameof(tailVertex));

            bool headIndex = Nodes.ContainsKey(headVertex);
            bool tailIndex = Nodes.ContainsKey(tailVertex);

            if (!headIndex)
            {
                DirectedGraphNode<T> nodeOne = new DirectedGraphNode<T>(headVertex);
                Nodes.Add(headVertex, nodeOne);
            }

            if (!tailIndex)
            {

                DirectedGraphNode<T> nodeTwo = new DirectedGraphNode<T>(tailVertex);
                Nodes.Add(tailVertex, nodeTwo);
            }


            Nodes[headVertex].AddNeighbor(Nodes[tailVertex]);
        }

        public void AddVertex(T vertex)
        {
            bool v2Index = Nodes.ContainsKey(vertex);

            if (!v2Index)
            {
                DirectedGraphNode<T> newNode = new DirectedGraphNode<T>(vertex);
                Nodes.Add(vertex, newNode);
            }
        }

        /// <summary>
        /// Removes an edge from the directed graph. The direction is from the head vertex to the tail vertex.
        /// </summary>
        /// <param name="headVertex"></param>
        /// <param name="tailVertex"></param>
        public void RemoveEdge(T headVertex, T tailVertex)
        {
            bool headIndex = Nodes.ContainsKey(headVertex);
            bool tailIndex = Nodes.ContainsKey(tailVertex);

            if (headIndex && tailIndex)
            {
                Nodes[tailVertex].RemoveNeighbor(Nodes[headVertex]);
            }

        }

        public void RemoveEdgeAndVertex(T vertexOne, T vertexTwo)
        {
            bool headIndex = Nodes.ContainsKey(vertexOne);
            bool tailIndex = Nodes.ContainsKey(vertexTwo);

            if (headIndex)
            {
                Nodes.Remove(vertexOne);
            }

            if (tailIndex)
            {
                Nodes.Remove(vertexTwo);
            }

            Nodes[vertexTwo].RemoveNeighbor(Nodes[vertexOne]);
        }


        /// <summary>
        /// Generic DFS function. Can be used to solve any problem that requires DFS, just plug in the inspectionFunc.
        /// </summary>
        /// <param name="inspectFunc">Must return true if the search is completed and no more nodes need to be checked</param>
        public void Dfs(Func<DirectedGraphNode<T>, bool> inspectFunc)
        {
            List<DirectedGraphNode<T>> nodes = Nodes.Values.ToList();
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
        private void InternalDfs(List<DirectedGraphNode<T>> nodes, bool[] visited, DirectedGraphNode<T> node, Func<DirectedGraphNode<T>, bool> inspectFunc)
        {
            int nodeId = nodes.IndexOf(node);
            visited[nodeId] = true;
            int childLoopItteration = 0;
            
           foreach (DirectedGraphNode<T> neighbor in node.GetNeighbors())
            {
                if (inspectFunc(neighbor))
                {
                    return;
                }
                if (!visited[nodes.IndexOf(neighbor)])
                {
                    InternalDfs(nodes, visited, neighbor, inspectFunc);
                }
                childLoopItteration++;
            }
        }

        /// <summary>
        /// Generic BFS function. Can be used to solve any problem that requires BFS, just plug in the inspectionFunc.
        /// </summary>
        /// <param name="inspectFunc">Must return true if the search is completed and no more nodes need to be checked</param>
        /// <param name="startId">Starting node, by default it is 0.</param>
        public void Bfs(Func<DirectedGraphNode<T>, bool> inspectFunc, bool countComponents)
        {
            List<DirectedGraphNode<T>> nodes = Nodes.Values.ToList();
            if (nodes.Count == 0)
            {
                return;
            }

            bool[] visited = new bool[nodes.Count];
            visited.Initialize();
            Queue<DirectedGraphNode<T>> nodeQueue = new Queue<DirectedGraphNode<T>>();
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
                        DirectedGraphNode<T> currentNode = nodeQueue.Dequeue();

                        if (inspectFunc != null && inspectFunc(currentNode))
                        {
                            return;
                        }

                        foreach (DirectedGraphNode<T> neighbor in currentNode.GetNeighbors())
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
