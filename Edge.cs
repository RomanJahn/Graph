using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public class Edge
    {
        public Edge(Vertex from, Vertex to, int distance, int capacity)
        {
            this.From = from ?? throw new ArgumentNullException(nameof(from));
            this.To = to ?? throw new ArgumentNullException(nameof(to));
            this.Distance = distance;
            this.Capacity = capacity;
        }
        public Vertex From { get; }

        public Vertex To { get; }

        public int Distance { get; }

        public int Capacity { get; }
    }
}
