using System;
using System.Collections.Generic;

namespace CustomStructures
{
    public class UniqueIntGraph:UndirectedGraph<int>
    {
        readonly Dictionary<int, UndirectedGraphNode<int>> m_DataValues = new Dictionary<int, UndirectedGraphNode<int>>();

        public UniqueIntGraph(int initialNodeData) : base(initialNodeData)
        {
            m_DataValues.Add(initialNodeData, Nodes[0]);
        }

        public void Add(int vertexOne, int vertexTwo)
        {
            UndirectedGraphNode<int> node1, node2;
            if (!m_DataValues.ContainsKey(vertexOne))
            {
                node1 = new UndirectedGraphNode<int>(vertexOne);
                m_DataValues.Add(vertexOne,node1);
            }
            else
            {
                node1 = this.Nodes.Find(item => item.Data == vertexOne);
            }

            if (!m_DataValues.ContainsKey(vertexTwo))
            {
                node2 = new UndirectedGraphNode<int>(vertexTwo);
                m_DataValues.Add(vertexTwo, node2);
            }
            else
            {
                node2 = this.Nodes.Find(item => item.Data == vertexTwo);
            }

            AddEdge(node1, node2);
        }

        
    }
}