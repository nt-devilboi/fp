using System.Drawing;

namespace TagsCloudVisualization;

public class ImageErrors
{
    public string SizeLessThanZero(Size size)
        => $"Size of image should be with positive number, now {size}";

    public string FontNotExists(string font)
        => $"Font: '{font}' not exists";

    public string WordOutSideImage()
        => $"The image is small for current count words";

    public string IsNotDirectory(string path) => 
        $"Path: {path}.  Should Be Directory. Add '/' in end";
}