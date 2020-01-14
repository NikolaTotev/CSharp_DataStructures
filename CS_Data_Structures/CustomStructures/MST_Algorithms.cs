using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomStructures
{
    public static class MST_Algorithms
    {
        public static void Kruskal(WeightedUndirectedGraph<string> graph)
        {
            DisjointSet set = new DisjointSet();
            foreach (var nodesKey in graph.Nodes.Keys)
            {
                set.MakeSet(nodesKey);
            }

            List<Edge> sortedEdges = graph.edges.OrderBy(x => x.Weight).ToList();

            foreach (var edge in sortedEdges)
            {
                if (set.FindSet(edge.Node1) != set.FindSet(edge.Node2))
                {
                    edge.PrintEdge();
                    set.Union(edge.Node1, edge.Node2);
                }
            }
        }

        public static void Prim(WeightedUndirectedGraph graph)
        {
            Dictionary<string, List<Connection>> Nodes = graph.Nodes;
            Dictionary<string, bool> selected = new Dictionary<string, bool>();

            foreach (var nodesKey in Nodes.Keys)
            {
                selected.Add(nodesKey, false);
            }


            selected[selected.Keys.ToList()[0]] = true;

            string node1 = "";
            string node2 = "";
            int weight = 0;
            int edgeNumber = 0;
            while (edgeNumber < Nodes.Count - 1)
            {
                int min = Int32.MaxValue;

                foreach (var key in Nodes.Keys)
                {
                    if (selected[key])
                    {
                        foreach (var connection in Nodes[key])
                        {
                            if (!selected[connection.Data])
                            {
                                if (connection.Weight < min)
                                {
                                    min = connection.Weight;
                                    node1 = key;
                                    node2 = connection.Data;
                                    weight = connection.Weight;
                                }
                            }
                        }
                    }
                }
                Console.WriteLine("{0} : {1} - {2}", node1, node2, weight.ToString());
                selected[node2] = true;
                edgeNumber++;
            }

        }
    }
}
