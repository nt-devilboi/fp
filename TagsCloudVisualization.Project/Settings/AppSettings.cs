namespace TagsCloudVisualization.Settings;

public sealed class AppSettings(TagCloudSettings tagCloudSettings, WordLoaderSettings wordLoaderSettings)
{
    public TagCloudSettings TagCloudSettings { get; } = tagCloudSettings;
    public WordLoaderSettings WordLoaderSettings { get; } = wordLoaderSettings;
}