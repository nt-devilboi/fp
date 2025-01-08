namespace TagsCloudVisualization;

public class StemErrors
{
    public string NotFoundInEnvVar()
        => $"Stem not set in variable 'Path'";

    public string TextIsEmptyOrOnlyBoringWords()
        => $"Text is empty or in text only boring words";
}