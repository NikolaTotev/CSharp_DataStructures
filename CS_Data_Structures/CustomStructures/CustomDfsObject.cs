using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomStructures
{
   public  class CustomDfsObject<T>
    {
        public DirectedGraphNode<T> ParentNode { get; set; }
        public DirectedGraphNode<T> ChildNode { get; set; }
        public int LoopItteration { get; set; }
    }
}
