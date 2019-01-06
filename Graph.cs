using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace Graphs
{
    public class Graph
    {
        public Graph(Vertex[] vertices, Edge[] edges)
        {
            this.Vertices = vertices ?? throw new ArgumentNullException(nameof(vertices));
            this.Edges = edges ?? throw new ArgumentNullException(nameof(edges));
        }

        public Vertex[] Vertices { get; }

        public Edge[] Edges { get; }

        public static Graph CreateGraphFromJsonFile(string filenameAbsolutePath)
        {
            if (!File.Exists(filenameAbsolutePath))
            {
                throw new ArgumentException("Couldn't find file.", nameof(filenameAbsolutePath));
            }

            string jsonString = File.ReadAllText(filenameAbsolutePath);

            Graph graph;

            try
            {
                graph = JsonConvert.DeserializeObject<Graph>(jsonString);
            }
            catch (Exception e)
            {
                throw new ArgumentException($"Couldn't read json file properly. Please check file.\nInnerException: {e.Message}", e);
            }
            

            foreach (var item in graph.Edges)
            {
                Vertex from = graph.Vertices.FirstOrDefault(x => x.Name == item.From.Name);
                Vertex to = graph.Vertices.FirstOrDefault(x => x.Name == item.To.Name);

                if (from != null && to != null)
                {
                    Vertex.CreateUndirectedEdge(from, to, item.Distance);
                }
            }

            return graph;
        }
    }
}
