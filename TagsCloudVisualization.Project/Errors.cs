namespace TagsCloudVisualization;

public static class Errors
{
    public static ImageErrors Image => new();
    public static StemErrors Stem => new();
    
    public static GenerateCloudError Cloud => new ();
}

public class GenerateCloudError
{
    public string WordOutsideImage()
    {
        return "The image is small for current count words";
    }

    public string ScopeMessage()
        => "Generate cloud error";
}