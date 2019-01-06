namespace Graphs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class EdmondsKarp
    {
        private Dictionary<int, Vertex> verticesAlloc;

        private int[,] capacities;

        private int[,] usedCapacity;

        private int[] parentsList;


        public int MaxFlow(Graph graph, Vertex source, Vertex sink)
        {
            if (graph == null) throw new ArgumentNullException(nameof(graph));

            if (source == null) throw new ArgumentNullException(nameof(source));

            if (sink == null) throw new ArgumentNullException(nameof(sink));

            if (!graph.Vertices.Any(x => x == source) || !graph.Vertices.Any(x => x == sink)) throw new ArgumentException("Source and sink must be in the graph!");

            this.verticesAlloc = new Dictionary<int, Vertex>();

            for (int i = 0; i < graph.Vertices.Length; i++)
            {
                this.verticesAlloc.Add(i, graph.Vertices[i]);
            }

            int n = graph.Vertices.Length;

            this.capacities = new int[n, n];
            this.usedCapacity = new int[n, n];
            this.parentsList = new int[n];

            foreach (var edge in graph.Edges)
            {
                int from = this.verticesAlloc.FirstOrDefault(x => x.Value.Name == edge.From.Name).Key;
                int to = this.verticesAlloc.FirstOrDefault(x => x.Value.Name == edge.To.Name).Key;

                capacities[from, to] = edge.Capacity;
                capacities[to, from] = edge.Capacity;
            }

            int start = this.verticesAlloc.FirstOrDefault(x => x.Value == source).Key;
            int end = this.verticesAlloc.FirstOrDefault(x => x.Value == sink).Key;

            int result = 0;

            while (true)
            {
                int currentFlow = this.BreadthFirstSearch(start, end);

                if (currentFlow == 0)
                {
                    break;
                }

                result += currentFlow;

                int current = end;

                // updating used capacity (both directions)
                while (current != start)
                {
                    int previous = this.parentsList[current];

                    this.usedCapacity[previous, current] += currentFlow;
                    this.usedCapacity[current, previous] -= currentFlow;
                    current = previous;
                }
            }

            return result;
        }

        private int BreadthFirstSearch(int start, int end)
        {
            for (int i = 0; i < this.parentsList.Length; i++)
            {
                this.parentsList[i] = -1;
            }

            //avoid getting back to start
            parentsList[start] = -2;

            int[] currentPathCapacity = new int[this.capacities.GetLength(0)];
            currentPathCapacity[start] = int.MaxValue;

            Queue<int> queue = new Queue<int>();
            queue.Enqueue(start);

            while (queue.Any())
            {
                int current = queue.Dequeue();

                foreach (var item in this.verticesAlloc[current].DistancesToNeighbours)
                {
                    int to = this.verticesAlloc.FirstOrDefault(x => x.Value == item.Key).Key;

                    if (this.parentsList[to] == -1)
                    {
                        if (this.capacities[current, to] - this.usedCapacity[current, to] > 0)
                        {
                            this.parentsList[to] = current;

                            currentPathCapacity[to] = Math.Min(currentPathCapacity[current], this.capacities[current, to] - this.usedCapacity[current, to]);

                            if (to == end)
                            {
                                return currentPathCapacity[end];
                            }

                            queue.Enqueue(to);
                        }
                    }
                }
            }

            return 0;
        }
    }
}
