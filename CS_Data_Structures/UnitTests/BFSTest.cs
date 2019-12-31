using System.Collections.Generic;
using System.Security.Claims;
using CustomStructures;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class BFSTest
    {
        private UndirectedGraph<int> m_TestIntGraph;
        private DirectedGraph<string> m_TestStringGraph;



        private void Ex1Setup()
        {
            m_TestStringGraph = new DirectedGraph<string>("LON");
            m_TestStringGraph.AddEdge("SOF", "LON");
            m_TestStringGraph.AddEdge("SOF", "PAR");
            m_TestStringGraph.AddEdge("PAR", "LSA");
            m_TestStringGraph.AddEdge("LON", "NYC");
            m_TestStringGraph.AddEdge("NYC", "SYD");
            m_TestStringGraph.AddEdge("SYD", "TOK");
            m_TestStringGraph.AddEdge("TOK", "PAR");
            m_TestStringGraph.AddEdge("PAR", "TOK");
            m_TestStringGraph.AddEdge("NYC", "SOF");
            m_TestStringGraph.AddEdge("SOF", "TOK");
        }

        private void Ex2Setup()
        {
            m_TestIntGraph = new UndirectedGraph<int>(1);
            m_TestIntGraph.AddEdge(1, 2);
            m_TestIntGraph.AddEdge(1, 3);
            m_TestIntGraph.AddEdge(2, 3);
            m_TestIntGraph.AddEdge(1, 4);
            m_TestIntGraph.AddEdge(2, 1);
            m_TestIntGraph.AddVertex(5);
            m_TestIntGraph.AddVertex(6);
            m_TestIntGraph.AddVertex(7);
            m_TestIntGraph.AddVertex(8);
            m_TestIntGraph.AddVertex(9);
            m_TestIntGraph.AddVertex(10);
        }


        [SetUp]
        public void InitFunc()
        {
            Ex1Setup();
        }

        public void CheckComponents()
        {
            int numberOfComponents = m_TestIntGraph.CountComponents();
            Assert.AreEqual(7, numberOfComponents, "Wrong answer!");
        }

        private int cityNumber = 0;
        List<string> cities = new List<string>();
        private int currentLoop = 0;
        private string startingCity = "LON";
        private bool CheckCity(CustomDfsObject<string> currentCity)
        {
            if (currentCity.LoopItteration > currentLoop)
            {
                cities.RemoveRange(cities.IndexOf(currentCity.ParentNode.Data) + 1, cities.Count - cities.IndexOf(currentCity.ParentNode.Data)+1);
                currentLoop = 0;
            }


            if (cities.Count > 0 && currentCity.ParentNode.Data == startingCity || currentCity.ChildNode.Data == startingCity)
            {
                if (currentCity.ParentNode.Data == startingCity)
                {
                    cities.Add(currentCity.ParentNode.Data);
                }

                if (currentCity.ChildNode.Data == startingCity)
                {
                    cities.Add(currentCity.ChildNode.Data);
                }

                return true;
            }


            if (!cities.Contains(currentCity.ParentNode.Data))
            {
                cities.Add(currentCity.ParentNode.Data);
            }

            if (!cities.Contains(currentCity.ChildNode.Data))
            {
                cities.Add(currentCity.ChildNode.Data);
            }


            cityNumber++;
            return false;
        }
        [Test]
        public void FindRoute()
        {
            m_TestStringGraph.Dfs(CheckCity);
            string finalPath = string.Join(" ", cities);
            Assert.AreEqual(" ", finalPath, finalPath);

        }


    }
}