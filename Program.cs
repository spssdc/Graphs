using System;
using System.Collections.Generic;

namespace Graphs
{
    class Program
    {

        static void Main()
        {
            // Create the instance of a graph called myGraph
            Graph myGraph = new Graph();
            List<string> myNodes = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H" };
            Console.WriteLine("Creating graph");

            // Add nodes names A--H to the graph
            foreach (string s in myNodes)
            {
                myGraph.AddNode(s);
            }
            myGraph.PrintNodes();

            // Add the edges
            myGraph.AddEdge("A", "B", 2);
            myGraph.AddEdge("A", "C", 10);
            myGraph.AddEdge("B", "D", 3);
            myGraph.AddEdge("C", "D", 2);
            myGraph.AddEdge("B", "E", 9);
            myGraph.AddEdge("D", "F", 11);
            myGraph.AddEdge("E", "F", 5);
            myGraph.AddEdge("C", "G", 8);
            myGraph.AddEdge("D", "G",7);
            myGraph.AddEdge("F", "H", 6);
            myGraph.AddEdge("G", "H", 1);

            Console.WriteLine("Graph created, algorithms next...");
            Console.WriteLine("BFS from A...");
            myGraph.BFS("A");
            Console.WriteLine("Shortest path from A to H");
            myGraph.Dijkstra("A", "H");
        }
    }
}
