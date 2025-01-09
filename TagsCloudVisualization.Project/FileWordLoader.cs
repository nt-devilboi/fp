using TagsCloudVisualization.Abstraction;
using TagsCloudVisualization.Extensions;
using TagsCloudVisualization.Result;

namespace TagsCloudVisualization;

internal sealed class FileWordLoader(
    FactoryStem FactorysteamReader)
    : IWordLoader
{
    public Result<ICollection<FrequencyWord>> LoadWords()
    {
        return FactorysteamReader.Create()
            .Then(Processing);
    }

    private static ICollection<FrequencyWord> Processing(IStemReader stemReader)
    {
        return stemReader
            .ReadLines()
            .Where(ValidateLexeme)
            .Select(GetLemma)
            .ToFrequencyPopular()
            .ToList();
    }

    private static bool ValidateLexeme(string x)
    {
        return !string.IsNullOrEmpty(x) && !IsBoring(x);
    }

    private static string GetLemma(string l)
    {
        return l.Split('=').First().ToLower();
    }

    private static bool IsBoring(string line)
    {
        return line.Contains("=PR=") || line.Contains("=PART=") || line.Contains("=CONJ=");
    }
}