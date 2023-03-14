using System;
using System.Collections.Generic;

namespace Graphs
{
    public class Graph
    {
        // nodeNames contains a list of the strings of node names A, B, C etc.
        private readonly List<string> nodeNames = new List<string>();
        // edgeConnections is a dictionary of nodeName to (start, end, weight) edge list
        private readonly Dictionary<string,List<Edge>> edgeConnections = new Dictionary<string,List<Edge>>();

        // Create a private class to implement edges in the graph
        private class Edge
        {
            public string Start { get; }
            public string End { get; }
            public int Weight { get; }

            public Edge(string s, string e, int w)
            {
                this.Start = s;
                this.End = e;
                this.Weight = w;
                Console.WriteLine("Adding: " + s + " to " + e + " weight " + w);
            }
        }
        public Graph()
        {
        }

        // AddNode allows a new nodeName to be added to the graph, e.g. A
        public void AddNode(string nodeName)
        {
            if (nodeNames.Contains(nodeName))
            {
                throw new InvalidOperationException("Node already exists");
            }
            else
            {
                // Add the new node name
                this.nodeNames.Add(nodeName);
            }
        }

        // AddEdge takes a (start, end, weight) edge and adds it to the dictionary
        // it does this both ways round for an undirected graph
        public void AddEdge(string start, string end, int weight)
        {
            // check start and end nodes exist
            if (nodeNames.Contains(start) && nodeNames.Contains(end))
            { 
                // Create an edge for start --> end
                Edge tempEdge = new Edge(start, end, weight);
                List<Edge> tempEdgeList = new List<Edge>();
                if (!edgeConnections.ContainsKey(start))
                {
                    // No entry yet, this is the first one in
                    tempEdgeList.Add(tempEdge);
                    this.edgeConnections.Add(start, tempEdgeList);
                }
                else
                {
                    // Retrieve existing list in dictionary values and append
                    tempEdgeList = edgeConnections[start];
                    tempEdgeList.Add(tempEdge);
                    this.edgeConnections[start] = tempEdgeList;
                }
                // Create an edge for end --> start (undirected graph)
                Edge tempEdge2 = new Edge(end, start, weight);
                List<Edge> tempEdgeList2 = new List<Edge>();
                if (!edgeConnections.ContainsKey(end))
                {
                    // No entry yet, this is the first one in
                    tempEdgeList2.Add(tempEdge2);
                    this.edgeConnections.Add(end, tempEdgeList2);
                }
                else
                {
                    // Retrieve existing list in dictionary values and append
                    tempEdgeList2 = edgeConnections[end];
                    tempEdgeList2.Add(tempEdge2);
                    this.edgeConnections[end] = tempEdgeList2;
                }
            }
            else
            {
                throw new InvalidOperationException("Node does not exist to connect");
            }
        }

        //  Test function to print out the node names in the graph
        public void PrintNodes()
        {
            foreach (string s in nodeNames)
            {
                Console.WriteLine(s);
            }
        }

        // Generate BFS from a given starting node name
        public void BFS(string s)
        {
            if (nodeNames.Contains(s))
            {
                Queue<string> toProcess = new Queue<string>();
                List<string> visited = new List<string>() { };

                // Initialise currentNode to given starting point
                string currentNode = s;
                // Add the currentNode to the queue to process
                toProcess.Enqueue(currentNode);
                while (toProcess.Count>0)
                {
                    // Check if currentNode has not been ticked as visited
                    if (!visited.Contains(currentNode))
                    {
                        // Add current node to visited list
                        visited.Add(currentNode);
                        // Add all connections to current node to queue toProcess
                        // Dictionary return has Edges (start, end, weight)
                        List<Edge> connections = edgeConnections[currentNode];
                        foreach (Edge e in connections)
                        {
                            if (!visited.Contains(e.End))
                            {
                                toProcess.Enqueue(e.End);
                            }
                        }
                    }
                    currentNode = toProcess.Dequeue();
                }
                visited.Add(currentNode);
                // Output the BFS result
                Console.WriteLine("Output visited nodes");
                foreach (string os in visited)
                {
                    Console.WriteLine(os);
                }
            }
            else
            {
                throw new InvalidOperationException("No such node...");
            }

        }

        // Dijkstras shortest path algorithm
        public void Dijkstra(string start, string goal)
        {
            // check start and goal nodes exist, otherwise throw exception
            if (nodeNames.Contains(start) && nodeNames.Contains(goal))
            { 
                // Set distances for all nodes to infinity
                Dictionary <string,int> distance = new Dictionary<string,int>();
                foreach (string node in this.nodeNames)
                {
                    distance[node] = int.MaxValue;
                }
                // Create dictionary for nodes previous node visited
                Dictionary<string, string> previous = new Dictionary<string, string>();
                // Set all as unvisited initially
                List<string> unvisited = this.nodeNames;
                distance[start] = 0;
                while (unvisited.Count>0)
                {
                    // find node with shortest distance from the start
                    string shortest = null;
                    foreach(string n in this.nodeNames)
                    {
                        if (shortest == null)
                        {
                            shortest = n;
                        }
                        else if(distance[n] < distance[shortest])
                        {
                            shortest = n;
                        }
                    }
                    // Visit all the points connected to shortest
                    // retrieving edge connections (Start,End,Weight) list
                    foreach(Edge e in this.edgeConnections[shortest])
                    {
                        int cost = e.Weight;
                        if (cost + distance[shortest] < distance[e.End])
                        {
                            distance[e.End] = cost + distance[shortest];
                            previous[e.End] = shortest;
                        }
                    }
                    unvisited.Remove(shortest);

                }
                List<string> path = new List<string>();
                string v = goal;
                while (v != start)
                {
                    path.Insert(0, v);
                    v = previous[v];
                }
                path.Insert(0, start);

                // Output results
                Console.WriteLine("Output Dikjstra's results...");
                foreach (string s in path)
                {
                    Console.WriteLine(s);
                }
            }
            else
            {
                throw new InvalidOperationException("Cannot process unknown nodes");
            }
        }
    }
}
