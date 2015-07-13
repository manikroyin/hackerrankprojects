using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRankAlgorithmicProjects
{
    public partial class Solution
    {
        public static void BreadthFirstSearchShortestReach()
        {
            const int edgeWeight = 6;
            // get number of tests
            int test = Convert.ToInt32(Console.ReadLine());
            for(int ti = 0; ti < test; ti++)
            {
                string nodeEdgeLine = Console.ReadLine();   // 1 <= T <= 10
                string[] neArr = nodeEdgeLine.Split(' ');   // 2 <= N <= 1000, 1 <= M <= N*(N-1)/2
                int numNodes = Convert.ToInt32(neArr[0]);
                int numEdges = Convert.ToInt32(neArr[1]);
                bool[,] edges = new bool[numNodes,numNodes];
                for (int ei = 0; ei < numEdges; ei++)
                {
                    string edge = Console.ReadLine();
                    string[] edgeArr = edge.Split(' ');     // 1 <= x, 1 <= y
                    int x = Convert.ToInt32(edgeArr[0]);
                    int y = Convert.ToInt32(edgeArr[1]);
                    edges[x - 1, y - 1] = true;
                    edges[y - 1, x - 1] = true;
                }
                bool[] visited = new bool[numNodes];
                int[] shortestDistances = new int[numNodes];
                for (int i = 0; i < numNodes; i++)
                    shortestDistances[i] = -1;
                int startNode = Convert.ToInt32(Console.ReadLine());    // S <= N
                shortestDistances[startNode - 1] = 0;
                Queue<int> q = new Queue<int>();
                q.Enqueue(startNode);
                // mark it as visited
                visited[startNode - 1] = true;
                while (q.Count > 0)
                {
                    // retrieve node
                    int node = q.Dequeue();
                    // enqueue the new neighbors and update their shortest distances
                    for(int nn = 0; nn < numNodes; nn++)
                    {
                        if (nn == node - 1 || visited[nn])  // exclude itself or if already visited
                            continue;
                        if (edges[node - 1, nn])
                        {
                            q.Enqueue(nn + 1);  // add a new edge node
                            shortestDistances[nn] = shortestDistances[node - 1] + edgeWeight;
                            visited[nn] = true;
                        }
                    }
                }
                for (int pi = 0; pi < numNodes; pi++)
                {
                    if (pi == startNode - 1) // skip start node
                        continue;
                    Console.Write("{0} ", shortestDistances[pi]);
                }
                Console.WriteLine();
            }
        }
    }
}
