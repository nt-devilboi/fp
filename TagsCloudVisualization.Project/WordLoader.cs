namespace TagsCloudVisualization;

public interface IWordLoader
{
    Result<ICollection<FrequencyWord>> LoadWords();
}