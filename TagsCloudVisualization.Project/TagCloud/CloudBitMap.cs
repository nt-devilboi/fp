using System.Drawing;
using System.Runtime.Versioning;
using TagsCloudVisualization.Abstraction;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization;

[SupportedOSPlatform("windows")]
public class CloudBitMap : ITagCloudImage, ITagCloudSave
{
    private readonly Bitmap bitmap;
    private readonly Graphics graphics;
    private readonly TagCloudSettings tagCloudSettings;
    private bool isDisposed;
    private bool isSave;


    public CloudBitMap(TagCloudSettings tagCloudSettings)
    {
        this.tagCloudSettings = tagCloudSettings;
        bitmap = new Bitmap(tagCloudSettings.Size.Width, tagCloudSettings.Size.Height);
        graphics = Graphics.FromImage(bitmap);
        graphics.Clear(this.tagCloudSettings.BackGround);
    }

    public Size Size()
    {
        return bitmap.Size;
    }

    public void DrawString(RectangleTagCloud rec)
    {
        var brush = new SolidBrush(tagCloudSettings.ColorWords);
        graphics.DrawString(rec.Text, GetFont(rec.EmSize), brush, rec.Rectangle);
    }

    public void Dispose()
    {
        if (!isDisposed)
        {
            bitmap.Dispose();
            graphics.Dispose();

            isDisposed = true;
        }
    }

    public void Save()
    {
        if (isSave)
        {
            Console.WriteLine("уже сохранена");
            return;
        }

        var saveFilePath = string.Join("", tagCloudSettings.PathDirectory,
            $"tagCloud-({tagCloudSettings.NamePhoto}).{tagCloudSettings.ImageFormat}");

        bitmap.Save(saveFilePath, tagCloudSettings.ImageFormat);
        Console.WriteLine($"file saved in {saveFilePath}");
        isSave = true;

        Dispose();
    }

    private Font GetFont(int emSize)
    {
        return new Font(tagCloudSettings.Font, emSize);
    }
}