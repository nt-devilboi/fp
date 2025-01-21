namespace TagsCloudVisualization;

public class StemErrors
{
    public string NotFoundInEnvVar()
    {
        return "Stem not set in variable 'Path' or PATH if you use unix-like";
    }

    public string TextIsEmptyOrOnlyBoringWords()
    {
        return "Text is empty or in text only boring words";
    }
}