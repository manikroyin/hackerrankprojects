using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRankAlgorithmicProjects
{
    public partial class Solution
    {
        public class dnode : IndexedData, IComparable<dnode>
        {
            public int nodeNum { get; set; }
            public int weight { get; set; }
            public dnode(int n, int w)
            {
                nodeNum = n;
                weight = w;
            }
            public int CompareTo(dnode other)
            {
                if (weight < other.weight) return -1;
                else if (weight > other.weight) return 1;
                else if (nodeNum < other.nodeNum) return -1;
                else return 1;
            }
        }
        public static int[] DijkstraShortestReach2(int numNodes, int[,] edges, int startNode)
        {
            int[] shortestDistances = new int[numNodes];
            bool[] visited = new bool[numNodes];
            dnode[] nodes = new dnode[numNodes];
            // initialize the queue
            PriorityQueue<dnode> q = new PriorityQueue<dnode>();
            for (int i = 0; i < numNodes; i++)
            {
                nodes[i] = new dnode(i + 1, Int32.MaxValue);
                if (i == startNode - 1)
                {
                    nodes[i].weight = 0;
                    shortestDistances[i] = 0;
                }
                else
                {
                    shortestDistances[i] = -1;
                }
                q.Enqueue(nodes[i]);
            }
            while (q.Count() > 0)
            {
                dnode top = q.Dequeue();
                if (top.weight == System.Int32.MaxValue)
                    break;
                // try update all the weights of not visited nodes directly connected by this node
                for (int j = 0; j < numNodes; j++)
                {
                    if (j != top.nodeNum - 1 && !visited[j] && edges[top.nodeNum - 1, j] > 0)  // finding neighbors
                    {
                        if (nodes[j].weight > top.weight + edges[top.nodeNum - 1, j])    //updating neighbor weights
                        {
                            nodes[j].weight = top.weight + edges[top.nodeNum - 1, j];
                            shortestDistances[j] = nodes[j].weight;
                            q.DecreasePriority(nodes[j]);
                        }
                    }
                }
                visited[top.nodeNum - 1] = true;
            }

            return shortestDistances;
        } // end DijkstraShortestReach2

        public static void DijkstraShortestReach2Runner()
        {
            char[] delimiters = new char[] { ' ' };
            // get number of tests
            int test = Convert.ToInt32(Console.ReadLine());
            for (int ti = 0; ti < test; ti++)
            {
                string nodeEdgeLine = Console.ReadLine();   // 1 <= T <= 10
                string[] neArr = nodeEdgeLine.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);   // 2 <= N <= 1000, 1 <= M <= N*(N-1)/2
                int numNodes = Convert.ToInt32(neArr[0]);
                int numEdges = Convert.ToInt32(neArr[1]);
                int[,] edges = new int[numNodes, numNodes];
                for (int i = 0; i < numNodes; i++)
                {
                    for (int j = 0; j < numNodes; j++)
                    {
                        edges[i, j] = -1;
                    }
                }
                for (int ei = 0; ei < numEdges; ei++)
                {
                    string edge = Console.ReadLine();
                    string[] edgeArr = edge.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);     // 1 <= x, 1 <= y
                    int x = Convert.ToInt32(edgeArr[0]);
                    int y = Convert.ToInt32(edgeArr[1]);
                    // there could be multiple edges - keep the smallest one
                    if (edges[x - 1, y - 1] == -1 || edges[x-1, y-1] > Convert.ToInt32(edgeArr[2]))
                    {
                        edges[x - 1, y - 1] = Convert.ToInt32(edgeArr[2]);
                        edges[y - 1, x - 1] = Convert.ToInt32(edgeArr[2]);
                    }
                }

                int startNode = Convert.ToInt32(Console.ReadLine());    // S <= N

                int[] shortestDistances = DijkstraShortestReach2(numNodes, edges, startNode);

                for (int pi = 0; pi < numNodes; pi++)
                {
                    if (pi == startNode - 1) // skip start node
                        continue;
                    Console.Write("{0} ", shortestDistances[pi]);
                }
                Console.WriteLine();
            }
        } // end DijkstraShortestReach2Runner
    }
}