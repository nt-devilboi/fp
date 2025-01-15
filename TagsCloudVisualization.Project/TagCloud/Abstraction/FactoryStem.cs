using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using TagsCloudVisualization.Extensions;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.Abstraction;

[SupportedOSPlatform("windows")]
[SupportedOSPlatform("linux")]
public class FactoryStem(WordLoaderSettings wordLoaderSettings)
{
    private static IStemReader CreateStem(WordLoaderSettings cloudSettings)
    {
        return new StemReader(cloudSettings);
    }

    public virtual Result<IStemReader> Create() // virtual для тестов
    {
        return ValidateMyStem(wordLoaderSettings)
            .Then(ValidTextFile)
            .Then(CreateStem);
    }

    private Result<WordLoaderSettings> ValidTextFile(WordLoaderSettings settings)
    {
        return settings.Validate(x => File.Exists(x.PathTextFile),
            x => $"File doesn't exist at the path  {x.PathTextFile}");
    }

    private Result<WordLoaderSettings> ValidateMyStem(WordLoaderSettings settings)
    {
        return settings.Validate(x => StemExists(), x => Errors.Stem.NotFoundInEnvVar());
    }


    [SupportedOSPlatform("windows")]
    [SupportedOSPlatform("linux")]
    private static bool StemExists()
    {
        var path = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "Path" : "PATH";

        return Environment.GetEnvironmentVariable(path)?
            .Split(";")
            .Any(x => File.Exists(Path.Combine(x, "mystem.exe"))) ?? false;
    }
}