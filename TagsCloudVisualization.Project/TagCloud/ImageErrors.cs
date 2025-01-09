using System.Drawing;

namespace TagsCloudVisualization;

public class ImageErrors
{
    public string ScopeMessage()
    {
        return "image has bad settings";
    }

    public string SizeLessThanZero(Size size)
    {
        return "Size of image should be with positive number, now {size}";
    }

    public string FontNotExists(string font)
    {
        return $"Font: '{font}' not exists";
    }

    public string WordOutSideImage()
    {
        return "The image is small for current count words";
    }

    public string IsNotDirectory(string path)
    {
        return $"Path: {path}.  Should Be Directory. Add '/' in end";
    }
}