using TagsCloudVisualization.Result;

namespace TagsCloudVisualization;

public interface IWordLoader
{
    Result<ICollection<FrequencyWord>> LoadWords();
}