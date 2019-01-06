namespace Graphs
{
    using System.Linq;

    public class DijkstraResult
    {
        public DijkstraResult(int shortestDistance, Vertex[] path)
        {
            this.ShortestDistance = shortestDistance;
            this.Path = path;
        }

        public int ShortestDistance { get; }

        public Vertex[] Path { get; }

        public override string ToString()
        {
            string result = "Path: ";

            foreach (var item in this.Path)
            {
                result += item.Name + " - ";
            }

            result = result.Substring(0, result.Length - 3);

            result += "\nTotal distance: " + this.ShortestDistance;

            return result;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            DijkstraResult other = obj as DijkstraResult;

            return this.ShortestDistance == other.ShortestDistance && this.Path.SequenceEqual(other.Path);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
