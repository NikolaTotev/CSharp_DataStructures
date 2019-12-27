﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CustomStructures
{
    public class UndirectedGraph<T>
    {
        public List<UndirectedGraphNode<T>> Nodes { get; set; }

        public UndirectedGraph(T initialNodeData)
        {
            UndirectedGraphNode<T> initialNode = new UndirectedGraphNode<T>(initialNodeData);
            Nodes.Add(initialNode);
        }

        public UndirectedGraph(UndirectedGraphNode<T> initialNode)
        {
            Nodes.Add(initialNode);
        }

        public void AddEdge(UndirectedGraphNode<T> vertexOne, UndirectedGraphNode<T> vertexTwo)
        {
            if (vertexOne == null) throw new ArgumentNullException(nameof(vertexOne));
            if (vertexTwo == null) throw new ArgumentNullException(nameof(vertexTwo));

            int v1Index = Nodes.IndexOf(vertexOne);
            int v2Index = Nodes.IndexOf(vertexTwo);

            if (v1Index == -1)
            {
                Nodes.Add(vertexOne);
            }

            if (v2Index == -1)
            {
                Nodes.Add(vertexTwo);
            }

            vertexTwo.AddNeighbor(vertexOne);
        }

        public void RemoveEdge(UndirectedGraphNode<T> vertexOne, UndirectedGraphNode<T> vertexTwo)
        {
            int v1Index = Nodes.IndexOf(vertexOne);
            int v2Index = Nodes.IndexOf(vertexTwo);

            vertexTwo.RemoveNeighbor(vertexOne);
        }

        public void RemoveEdgeAndVertex(UndirectedGraphNode<T> vertexOne, UndirectedGraphNode<T> vertexTwo)
        {
            int v1Index = Nodes.IndexOf(vertexOne);
            int v2Index = Nodes.IndexOf(vertexTwo);

            if (v1Index == -1)
            {
                Nodes.Remove(vertexOne);
            }

            if (v2Index == -1)
            {
                Nodes.Remove(vertexTwo);
            }

            vertexTwo.RemoveNeighbor(vertexOne);
        }


        /// <summary>
        /// Generic DFS function. Can be used to solve any problem that requires DFS, just plug in the inspectionFunc.
        /// </summary>
        /// <param name="inspectFunc">Must return true if the search is completed and no more nodes need to be checked</param>
        public void Dfs(Func<UndirectedGraphNode<T>, bool> inspectFunc)
        {
            if (Nodes.Count == 0)
            {
                return;
            }
            bool[] visited = new bool[Nodes.Count];
            visited.Initialize();

            foreach (var node in Nodes)
            {
                InternalDfs(visited, node, inspectFunc);
            }
        }
        private void InternalDfs(bool[] visited, UndirectedGraphNode<T> node, Func<UndirectedGraphNode<T>, bool> inspectFunc)
        {
            int nodeId = Nodes.IndexOf(node);
            visited[nodeId] = true;

            if (inspectFunc(node))
            {
                return;
            }
            foreach (UndirectedGraphNode<T> neighbor in node.GetNeighbors())
            {
                if (!visited[Nodes.IndexOf(neighbor)])
                {
                    InternalDfs(visited, neighbor, inspectFunc);
                }
            }
        }

        /// <summary>
        /// Generic BFS function. Can be used to solve any problem that requires BFS, just plug in the inspectionFunc.
        /// </summary>
        /// <param name="inspectFunc">Must return true if the search is completed and no more nodes need to be checked</param>
        /// <param name="startId">Starting node, by default it is 0.</param>
        public void Bfs(Func<UndirectedGraphNode<T>, bool> inspectFunc,)
        {
            if (Nodes.Count == 0)
            {
                return;
            }
            bool[] visited = new bool[Nodes.Count];
            visited.Initialize();

            Queue<UndirectedGraphNode<T>> nodeQueue = new Queue<UndirectedGraphNode<T>>();
            foreach (var undirectedGraphNode in Nodes)
            {
                int currentNodeId = Nodes.IndexOf(undirectedGraphNode);
                if (!visited[currentNodeId])
                {
                    nodeQueue.Enqueue(undirectedGraphNode);
                    visited[currentNodeId] = true;
                    while (nodeQueue.Count != 0)
                    {
                        UndirectedGraphNode<T> currentNode = nodeQueue.Dequeue();

                        if (inspectFunc(currentNode))
                        {
                            return;
                        }

                        foreach (UndirectedGraphNode<T> neighbor in currentNode.GetNeighbors())
                        {
                            if (!visited[Nodes.IndexOf(neighbor)])
                            {
                                visited[Nodes.IndexOf(neighbor)] = true;
                                nodeQueue.Enqueue(neighbor);
                            }
                        }

                    }
                }

            }
        }
    }
}
