using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomStructures
{
    //Todo make with generics.
    class DisjointSet
    {
        private Dictionary<string, string> Parent { get; set; }
        public Dictionary<string, int> Rank { get; set; }

        public DisjointSet()
        {
            Parent = new Dictionary<string, string>();
            Rank = new Dictionary<string, int>();
        }


        public void MakeSet(string x)
        {
            Parent[x] = x;
            Rank[x] = 0;
        }

        public string FindSet(string x)
        {
            if (Parent[x] != x)
            {
                Parent[x] = FindSet(Parent[x]);
            }

            return Parent[x];
        }
        public void Union(string x, string y)
        {
            string representativeX = FindSet(x);
            string representativeY = FindSet(y);

            if (Rank[representativeX] == Rank[representativeY])
            {
                Rank[representativeY]++;
                Parent[representativeX] = representativeY;
            }

            if (Rank[representativeX] > Rank[representativeY])
            {
                Parent[representativeY] = representativeX;
            }
            else
            {
                Parent[representativeX] = representativeY;
            }
        }

        public string FindImmediateParent(string x)
        {
            return Parent[x];
        }

        public int FindRank(string x)
        {
            return Rank[x];
        }

    }
}
