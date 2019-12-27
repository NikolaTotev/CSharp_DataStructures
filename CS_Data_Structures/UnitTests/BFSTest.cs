using System.Security.Claims;
using CustomStructures;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class BFSTest
    { 
        UndirectedGraph<int> TestGraph = new UndirectedGraph<int>(10);
        [SetUp]
        public void InitFunc()
        {
            TestGraph.AddEdge(10,20);
        }

        public void callPrint()
        {

        }
        public void TestBfs()
        {
           
        }
        

    }
}