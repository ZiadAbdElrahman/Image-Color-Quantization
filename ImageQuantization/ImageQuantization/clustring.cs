using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageQuantization
{

    class Edge : IComparable<Edge>
    {
        public int src, dest;

        public double weight;
        public int CompareTo(Edge compareEdge)
        {
            return weight.CompareTo(compareEdge.weight);
        }
        public Edge(int src, int dest, double weight) {
            this.src = src;
            this.dest = dest;
            this.weight = weight;

        }

    }
   
    class clustring
    {
        public static double similarity(RGBPixel c1, RGBPixel c2)
        {

            return Math.Sqrt(Math.Pow(c1.red - c2.red, 2) + Math.Pow(c1.green - c2.green, 2) + Math.Pow(c1.blue - c2.blue, 2));

        }
        public static Dictionary<int, RGBPixel> getColors(RGBPixel[,] image)
        {
            Dictionary<int, RGBPixel > colors = new Dictionary<int, RGBPixel>();
            int counter = 0;
            for (int i = 0; i < ImageOperations.GetHeight(image); i++)
                for (int j = 0; j < ImageOperations.GetWidth(image); j++)
                    if (!colors.ContainsValue(image[i, j]))
                    {
                        //Console.WriteLine(image[i, j].red + "  " + image[i, j].green + "  " + image[i, j].blue );
                        colors.Add(counter++, image[i, j]);
                    }
            return colors;
        }
        public static List<Edge> getGraph(Dictionary<int, RGBPixel> colors)
        {
            List<Edge> graph= new List<Edge>();
            
            for (int i = 0; i < colors.Count; i++)
                for (int j = 0; j < i; j++)
                {
                    graph.Add(new Edge(i, j, similarity(colors[i], colors[j])));
                }
            return graph;
        }


        public static List<Edge> clustr_graph(List<Edge> graph, int k) {
            for (int i = 0; i < k-1; i++) {
                int maxEdge = 0;
                for (int j= 0;j < graph.Count;j++) {
                    if (graph[j].weight > graph[maxEdge].weight)
                        maxEdge = j;
                }
                graph.RemoveAt(maxEdge);
            }
            return graph;
        }
        public static List<int>[] ToAdjacencylists(List<Edge> graph, int v) {
            List<int>[] adjGraph = new List<int>[v];
            for (int i = 0; i < v; i++)
                adjGraph[i] = new List<int>();
            for (int i = 0; i < graph.Count; i++) {
                adjGraph[graph[i].dest].Add(graph[i].src);
                adjGraph[graph[i].src].Add(graph[i].dest);
            }
            return adjGraph;
        }
        public static RGBPixel calculat_avg(List<int> sub, Dictionary<int, RGBPixel> dic) {
            RGBPixelD color; color.red = 0; color.green = 0; color.blue = 0;
            for (int j = 0; j < sub.Count; j++)
            {
                color.red += dic[sub[j]].red;
                color.green += dic[sub[j]].green;
                color.blue += dic[sub[j]].blue;
            }
            color.red /= sub.Count; color.green /= sub.Count; color.blue /= sub.Count;
            RGBPixel c;
            
            c.red = (byte)color.red;
            c.green = (byte)color.green;
            c.blue = (byte)color.blue;
            return c;
        }
        public static Dictionary<RGBPixel, RGBPixel> NewColors(List<int>[]graph, Dictionary<int, RGBPixel> dic) {
            Dictionary<RGBPixel, RGBPixel> color_converter = new Dictionary<RGBPixel, RGBPixel>(); 
            bool[] visted = new bool[graph.Length];
           
            for (int i = 0; i < graph.Length; i++) {
                
                if (!visted[i]){
                    List<int> sub = new List<int>();
                    Queue<int> dfs = new Queue<int>();
                    List<RGBPixel> Oldcolors = new List<RGBPixel>();
                    dfs.Enqueue(i);
                
                    while (dfs.Count != 0)
                    {
                        int q = dfs.Dequeue();
                        visted[q] = true;
                        sub.Add(q);
                        Oldcolors.Add(dic[q]);
                        for (int j = 0; j < graph[q].Count; j++)
                        {
                            if (!visted[graph[q][j]])
                                dfs.Enqueue(graph[q][j]);
                        }
                    }
                    RGBPixel newcolor = calculat_avg(sub, dic);
                    for (int j = 0; j < Oldcolors.Count; j++) {
                        color_converter[Oldcolors[j]] = newcolor;
                    }
                    
                }
            }

            return color_converter;
        }
        public static RGBPixel[,] getNewImage(Dictionary<RGBPixel, RGBPixel> converter, RGBPixel[,] image) {
            int h = ImageOperations.GetHeight(image), w = ImageOperations.GetWidth(image);
            RGBPixel[,] newImage = new RGBPixel[h,w];

            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                    newImage[i, j] = converter[image[i, j]];
            return newImage;
        }
        public static RGBPixel[,] clustr_image(RGBPixel[,] ImageMatrix, int k)
        {
            
            RGBPixel[,] NewImage = ImageMatrix;
            Dictionary<int, RGBPixel> colors = getColors(ImageMatrix);
            
            Console.WriteLine(colors.Count);
            List<Edge> graph = getGraph(colors);
            Console.WriteLine(graph.Count);
            List<Edge> mst = MST.getMST(graph, colors.Count);
            for (int i = 0; i < mst.Count; i++) {
                
            }
            Console.WriteLine(mst.Count);
            List<Edge> clusterd_graph = clustr_graph(mst, k);
            Console.WriteLine(clusterd_graph.Count);
            List<int>[] AdjGraph = ToAdjacencylists(clusterd_graph, colors.Count);

            Dictionary<RGBPixel, RGBPixel> color_converter = NewColors(AdjGraph, colors);

            NewImage = getNewImage(color_converter, ImageMatrix);


            return NewImage;

        }
    }
}
