namespace Graphs
{
    using System;
    using System.Collections.Generic;

    public class Vertex
    {
        public Vertex(string name)
        {
            this.Name = name;
            this.DistancesToNeighbours = new Dictionary<Vertex, int>();
        }

        public string Name { get; }

        public int TotalDistance { get; set; }

        public Vertex Previous { get; set; }

        public Dictionary<Vertex, int> DistancesToNeighbours { get; private set; }

        public bool Visited { get; set; }

        public override string ToString()
        {
            return this.Name;
        }

        public static void CreateUndirectedEdge(Vertex v1, Vertex v2, int distance)
        {
            if (v1 == null)
            {
                throw new ArgumentNullException(nameof(v1));
            }

            if (v2 == null)
            {
                throw new ArgumentNullException(nameof(v2));
            }

            if (distance < 1)
            {
                throw new ArgumentException("The distance between 2 vertices must be at least 1", nameof(distance));
            }

            //evtl try catch wg. doppelten key... (mit bool ob überschrieben werden soll)
            v1.DistancesToNeighbours.Add(v2, distance);
            v2.DistancesToNeighbours.Add(v1, distance);
        }
    }
}
