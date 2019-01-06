namespace Graphs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Dijkstra
    {
        public static DijkstraResult ShortestPath(Graph graph, Vertex source, Vertex destination)
        {
            if (graph == null) throw new ArgumentNullException(nameof(graph));

            if (source == null) throw new ArgumentNullException(nameof(source));

            if (destination == null) throw new ArgumentNullException(nameof(destination));

            if (!graph.Vertices.Any(x => x == source) || !graph.Vertices.Any(x => x == destination)) throw new ArgumentException("Source and destination must be in the graph!");

            foreach (var vertex in graph.Vertices)
            {
                vertex.TotalDistance = 0;
                vertex.Visited = false;
                vertex.Previous = null;
            }

            Vertex currentVertex = source;

            bool foundShortestPath = false;

            while (!foundShortestPath)
            {
                currentVertex.Visited = true;

                // for each neighbour
                foreach (var item in currentVertex.DistancesToNeighbours)
                {
                    Vertex neighbourVertex = item.Key;

                    if (neighbourVertex.Visited)
                    {
                        continue;
                    }

                    //calculate total distance to neighbour
                    int possibleNewTotalDistanceForNeighbour = currentVertex.TotalDistance + item.Value;

                    // if new calculated distance is shorter (or neighbours distance is 0 -> neighbours distance was never actualized)
                    if (possibleNewTotalDistanceForNeighbour < neighbourVertex.TotalDistance || neighbourVertex.TotalDistance == 0)
                    {
                        //set the new (shorter) distance
                        neighbourVertex.TotalDistance = possibleNewTotalDistanceForNeighbour;

                        // set the previous vertex (for "saving" the current shortest path)
                        neighbourVertex.Previous = currentVertex;
                    }
                }

                // find minimum total distance of all not visited vertices (except never updated vertices)
                int minDistance = graph.Vertices.Where(x => !x.Visited && x.TotalDistance > 0).Min(x => x.TotalDistance);

                // move to vertex with minimum total distance
                currentVertex = graph.Vertices.FirstOrDefault(x => x.TotalDistance == minDistance && !x.Visited);

                // if vertex with minimum total distance is destination - all other path are longer (or equal)
                if (currentVertex == destination)
                {
                    foundShortestPath = true;

                } 
            }

            int totalShortestDistance = currentVertex.TotalDistance;

            List<Vertex> path = new List<Vertex>();

            path.Add(currentVertex);

            while (currentVertex.Previous != null)
            {
                path.Insert(0, currentVertex.Previous);
                currentVertex = currentVertex.Previous;
            }

            return new DijkstraResult(totalShortestDistance, path.ToArray());
        }
    }
}
