using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageQuantization
{
    public class SubTree
    {
        public int parent, power;
    }
    class MST
    {
        public static int getRoot(SubTree[] subtree, int ver)
        {
            if (subtree[ver].parent != ver)
                subtree[ver].parent = getRoot(subtree, subtree[ver].parent);

            return subtree[ver].parent;
        }

        public static void addLeaf(SubTree[] subtree, int src, int dest)
        {
            int srcroot = getRoot(subtree, src);
            int destroot = getRoot(subtree, dest);

            if (subtree[srcroot].power < subtree[destroot].power)
            {
                subtree[srcroot].parent = destroot;
            }
            else if (subtree[srcroot].power > subtree[destroot].power)
            {
                subtree[destroot].parent = srcroot;
            }
            else
            {
                subtree[srcroot].parent = destroot;
                subtree[destroot].power++;
            }
        }
        public static List<Edge> getMST(List<Edge> graph, int v)
        {
            graph.Sort();
            List<Edge> result = new List<Edge>();

            SubTree[] subtree = new SubTree[v];
            for (int i = 0; i < v; i++)
            {
                subtree[i] = new SubTree();
                subtree[i].parent = i;
                subtree[i].power = 0;
            }
            int counter = 0;
            while (result.Count < v - 1)
            {
                Edge e = graph[counter++];
                int srcRoot = getRoot(subtree, e.src);
                int destRoot = getRoot(subtree, e.dest);
                if (srcRoot != destRoot)
                {
                    result.Add(e);
                    addLeaf(subtree, srcRoot, destRoot);
                }

            }

            return result;
        }
    }
}
