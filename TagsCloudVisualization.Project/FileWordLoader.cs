using System.Runtime.Versioning;
using TagsCloudVisualization.Abstraction;
using TagsCloudVisualization.Extensions;

namespace TagsCloudVisualization;

[SupportedOSPlatform("windows")]
[SupportedOSPlatform("linux")]
internal sealed class FileWordLoader(
    FactoryStem factorySteamReader)
    : IWordLoader
{
    public Result<ICollection<FrequencyWord>> LoadWords()
    {
        return factorySteamReader.Create()
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