using System.Drawing.Imaging;
using System.Runtime.Versioning;

namespace TagsCloudVisualization.Extensions;

[SupportedOSPlatform("windows")]
public static class ImageFormatFrom
{
    public static ImageFormat? String(string format)
    {
        return format.ToLower() switch
        {
            "png" => ImageFormat.Png,
            "jpg" => ImageFormat.Jpeg,
            "bpm" => ImageFormat.Bmp,
            _ => null
        };
    }
}