using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.Versioning;
using CommandLine;
using TagsCloudVisualization.Result;

namespace TagCloud2.Options;

[Verb("create")]
[SupportedOSPlatform("windows")]
public class CreateTagCloud
{
    [Option('w', "PathWordsFile", Required = true, HelpText = "path of file with words")]
    public required string PathToWords { get; init; }

    [Option('s', "size", Required = true, HelpText = "size of image formate WxH", Separator = 'x')]
    public required IEnumerable<string> Size { private get; init; }

    [Option('d', "pathDirectory", Required = true, HelpText = "path of directory for photos", Group = "path")]
    public required string Directory { get; init; }

    [Option('n', "NameFile", Required = true, HelpText = "Name of photos")]
    public required string NamePhoto { get; init; }


    [Option('e', "emSize", Required = true, HelpText = "Max EmSize of word")]
    public required int EmSize { get; init; }

    [Option('c', "Color", Required = false, HelpText = "Color of words")]
    public Color Color { get; init; } = Color.Black;

    [Option('b', "background", Required = false, HelpText = "Color of words")]
    public Color BackgrondColor { get; init; } = Color.White;

    [Option('f', "format", Required = false, HelpText = "Format of Photo")]
    public string ImageFormatString { private get; init; } = "png";

    [Option('t', "typeFace", Required = false, HelpText = "font of words")]
    public string Font { get; init; } = "arial";

    // хотел это (GetImageFormat) перенести в другое место, а в настройках хранить строку и потом преобразовывать, но в таком случае нужно будет всегда смотреть какой Result. то есть постоянно вычислять imageFormat и оставил так. лучше будет один gateway для преобразование(здесь) чем в нескольких местах его вставлять
    public Result<ImageFormat> GetImageFormat()
    {
        return ImageFormatString.ToLower() switch
        {
            "png" => ImageFormat.Png,
            "jpg" => ImageFormat.Jpeg,
            "bpm" => ImageFormat.Bmp,
            _ => Result.Fail<ImageFormat>(
                $"Image format '{ImageFormatString.ToLower()}' doesn't exist. you can use only png, jpeg or bpm")
        };
    }

    public Size GetSize()
    {
        var size = Size.ToArray();

        return new Size(int.Parse(size[0]), int.Parse(size[1]));
    }
}