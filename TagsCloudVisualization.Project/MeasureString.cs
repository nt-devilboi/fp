using System.Drawing;
using System.Runtime.Versioning;
using TagsCloudVisualization.Abstraction;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization;

[SupportedOSPlatform("windows")]
internal class MeasureString(TagCloudSettings tagCloudSettings) : ISizeWord
{
    private readonly Graphics graphics = Graphics.FromImage(new Bitmap(1, 1));

    public Size GetSizeWord(string word, int emSize)
    {
        return graphics.MeasureString(word, GetFont(emSize)).ToSize();
    }

    private Font GetFont(int emSize)
    {
        return new Font(tagCloudSettings.Font, emSize);
    }
}