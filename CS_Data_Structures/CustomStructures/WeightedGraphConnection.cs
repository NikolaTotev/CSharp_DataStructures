using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomStructures
{
    public class WeightedGraphConnection<T,W>
    {
        public WeightedUndirectedGraphNode<T,W> ConnectionNeighbor { get; set; }
        public W ConnectionWeight { get; set; }

        public WeightedGraphConnection(WeightedUndirectedGraphNode<T,W> connectionNeighbor, W weight)
        {
            ConnectionNeighbor = connectionNeighbor;
            ConnectionWeight = weight;
        }

    }
}
