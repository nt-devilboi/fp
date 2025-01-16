namespace TagsCloudVisualization.Abstraction;

public interface IStemReader : IDisposable
{
    public string? ReadLine();

    public IEnumerable<string> ReadLines();
}