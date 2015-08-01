using System;
using System.Collections.Generic;

namespace HackerRankAlgorithmicProjects
{
    public partial class Solution
    {
        public class Edge : IndexedData, IComparable<Edge>
        {
            public int firstNode { get; set; }
            public int secondNode { get; set; }
            public int weight { get; set; }
            public Edge(int firstNode, int secondNode, int weight)
            {
                this.firstNode = firstNode;
                this.secondNode = secondNode;
                this.weight = weight;
            }
            public int CompareTo(Edge other)
            {
                if (this.weight < other.weight)
                    return -1;
                else if (this.weight > other.weight)
                    return 1;
                else if (this.firstNode < other.firstNode)
                    return -1;
                else if (this.firstNode > other.firstNode)
                    return 1;
                else
                    return 0;
            }
        }
        public static List<Edge> PrimsMSTSpecialSubtree(int numNodes, int[,] edges, int startNode)
        {
            HashSet<int> visited = new HashSet<int>();
            // create a priority queue with edge weight as priority
            PriorityQueue<Edge> edgeQ = new PriorityQueue<Edge>();
            visited.Add(startNode);
            // add all the edges of start node
            for(int j = 0; j < numNodes; j++)
            {
                if(startNode != (j + 1) && edges[startNode - 1, j] != -1)
                    edgeQ.Enqueue(new Edge(startNode, j+1, edges[startNode - 1, j]));
            }
            List<Edge> selectedEdges = new List<Edge>();
            while(visited.Count < numNodes)
            {
                var candidate = edgeQ.Dequeue();
                if(visited.Contains(candidate.firstNode) && visited.Contains(candidate.secondNode))
                {
                    // we retrieved one edge whose nodes are already in, ignore this
                    continue;
                }
                selectedEdges.Add(candidate);
                // add the new node to visited set
                int newNode = candidate.firstNode;
                if(visited.Contains(candidate.firstNode))
                {
                    newNode = candidate.secondNode;
                }
                visited.Add(newNode);
                for(int i = 0; i < numNodes; i++)
                {
                    if(newNode != (i+1) && !visited.Contains(i+1) && edges[newNode - 1, i] != -1)
                    {
                        edgeQ.Enqueue(new Edge(newNode, i + 1, edges[newNode - 1, i]));
                    }
                }
            }
            return selectedEdges;
        }
        public static void PrimsMSTSpecialSubtreeRunner()
        {
            char[] delimiters = new char[] { ' ' };
            string nodeEdgeLine = Console.ReadLine();   // 2 <= N <= 3000, 1 <= M <= (N * (N - 1))/2
            string[] neArr = nodeEdgeLine.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);   // 2 <= N <= 1000, 1 <= M <= N*(N-1)/2
            int numNodes = Convert.ToInt32(neArr[0]);
            int numEdges = Convert.ToInt32(neArr[1]);
            // adjacency matrix of the graph
            int[,] edges = new int[numNodes, numNodes];
            for (int i = 0; i < numNodes; i++)
            {
                for (int j = 0; j < numNodes; j++)
                {
                    edges[i, j] = -1;
                }
            }
            // read the edge list from input
            for (int ei = 0; ei < numEdges; ei++)
            {
                string edge = Console.ReadLine();
                string[] edgeArr = edge.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);     // 1 <= x, 1 <= y
                int x = Convert.ToInt32(edgeArr[0]);
                int y = Convert.ToInt32(edgeArr[1]);
                // there could be multiple edges - keep the smallest one
                if (edges[x - 1, y - 1] == -1 || edges[x - 1, y - 1] > Convert.ToInt32(edgeArr[2]))
                {
                    edges[x - 1, y - 1] = Convert.ToInt32(edgeArr[2]);
                    edges[y - 1, x - 1] = Convert.ToInt32(edgeArr[2]);
                }
            }
            // read the start node
            int startNode = Convert.ToInt32(Console.ReadLine());    // S <= N
            int edgeSumMST = 0;
            List<Edge> edgeList = PrimsMSTSpecialSubtree(numNodes, edges, startNode);

            for (int ei = 0; ei < edgeList.Count; ei++)
            {
                edgeSumMST += edgeList[ei].weight;
            }
            Console.WriteLine("{0}", edgeSumMST);
        } // end PrimsMSTSpecialSubtreeRunner
    }
}
