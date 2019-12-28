using System.Security.Claims;
using CustomStructures;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class BFSTest
    {
        private UndirectedGraph<int> m_TestGraph;
        [SetUp]
        public void InitFunc()
        {
            m_TestGraph = new UndirectedGraph<int>(1);
            m_TestGraph.AddEdge(1 ,2);
            m_TestGraph.AddEdge(1 ,3);
            m_TestGraph.AddEdge(2 ,3);
            m_TestGraph.AddEdge(1 ,4);
            m_TestGraph.AddEdge(2,1);
            m_TestGraph.AddVertex(5);
            m_TestGraph.AddVertex(6);
            m_TestGraph.AddVertex(7);
            m_TestGraph.AddVertex(8);
            m_TestGraph.AddVertex(9);
            m_TestGraph.AddVertex(10);
        }

        [Test]
        public void CheckComponents()
        {
            int numberOfComponents = m_TestGraph.CountComponents();
           Assert.AreEqual(7, numberOfComponents, "Wrong answer!" );
        }
        

    }
}