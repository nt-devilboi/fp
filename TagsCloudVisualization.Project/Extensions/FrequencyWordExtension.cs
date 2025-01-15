namespace TagsCloudVisualization.Extensions;

public static class FrequencyWordExtension
{
    public static IEnumerable<FrequencyWord> ToFrequencyPopular(this IEnumerable<string> s)
    {
        return s.GroupBy(x => x)
            .Select(x => new FrequencyWord(x.Key, x.Count()));
    }
}