namespace TagsCloudVisualization.Extensions;

public static class PopularWordExtension
{
    public static IEnumerable<FrequencyWord> ToFrequencyPopular(this IEnumerable<string> s) => 
        s.
            GroupBy(x => x)
            .Select(x => new FrequencyWord(x.Key, x.Count()));
}