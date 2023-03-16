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
            List<string> myNodes = new List<string> { "A", "B", "C", "D", "E", "F", "G" };
            Console.WriteLine("\nCreating graph\n");

            // Add nodes names A--H to the graph
            foreach (string s in myNodes)
            {
                myGraph.AddNode(s);
            }
            Console.WriteLine("\nAdding nodes\n");
            myGraph.PrintNodes();

            // Add the edges
            Console.WriteLine("\nAdding Edges\n");
            myGraph.AddEdge("B", "D", 400);
            myGraph.AddEdge("B", "A", 700);
            myGraph.AddEdge("D", "C", 200);
            myGraph.AddEdge("C", "F", 300);
            myGraph.AddEdge("C", "E", 200);
            myGraph.AddEdge("A", "C", 500);
            myGraph.AddEdge("E", "G", 500);
            myGraph.AddEdge("A", "E", 600);

            Console.WriteLine("\nGraph created, algorithms next...");
            Console.WriteLine("BFS from B...");
            myGraph.BFS("B");
            Console.WriteLine("\nGraph created, algorithms next...");
            Console.WriteLine("DFS from B...");
            myGraph.DFS("B");
            Console.WriteLine("\nShortest path from B to G");
            myGraph.Dijkstra("B", "G");
        }
    }
}
