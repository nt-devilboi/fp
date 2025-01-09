namespace TagsCloudVisualization;

public interface IWordLoader
{
    IEnumerable<FrequencyWord> LoadWords();
}