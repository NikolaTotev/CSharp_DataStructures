using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomStructures
{
    public class Edge
    {
        public string Node1 { get; set; }
        public string Node2 { get; set; }
        public int Weight { get; set; }

        public Edge(string node1, string node2, int weight)
        {
            Node1 = node1;
            Node2 = node2;
            Weight = weight;
        }

        public void PrintEdge()
        {
            Console.WriteLine("{0} : {1} - {2}", Node1, Node2, Weight.ToString());
        }
    }
}
