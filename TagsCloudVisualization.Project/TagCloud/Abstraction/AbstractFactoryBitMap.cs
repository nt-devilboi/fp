using System.Drawing.Text;
using TagsCloudVisualization.Extensions;
using TagsCloudVisualization.Result;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.Abstraction;

public abstract class AbstractFactoryBitMap(TagCloudSettings cloudSettings)
{
    protected abstract ITagCloudImage CreateBitMap(TagCloudSettings cloudSettings);

    public Result<ITagCloudImage> Create()
    {
        return cloudSettings.AsResult().Then(ValidatePathNamed)
            .Then(ValidateSizeImage)
            .Then(ValidateFullPath)
            .Then(ValidateDirectory)
            .Then(FontExists)
            .Then(CreateBitMap)
            .RefineError("Image has bad settings");
    }

    private Result<TagCloudSettings> ValidateDirectory(TagCloudSettings tagCloudSettings)
    {
        return tagCloudSettings.Validate(t
                => Directory.Exists(t.PathDirectory),
            t =>
                $"This directory not exists  {t.PathDirectory}");
    }

    private Result<TagCloudSettings> ValidateSizeImage(TagCloudSettings tagCloudSettings)
    {
        return tagCloudSettings.Validate(t
            => t.Size is { Width: > 0, Height: > 0 }, t => Errors.Image.SizeLessThanZero(t.Size));
    }

    private Result<TagCloudSettings> ValidatePathNamed(TagCloudSettings tagCloudSettings)
    {
        return tagCloudSettings.Validate(t
            => t.PathDirectory.EndsWith('/'), t => Errors.Image.IsNotDirectory(t.PathDirectory));
    }

    private Result<TagCloudSettings> ValidateFullPath(TagCloudSettings tagCloudSettings)
    {
        return tagCloudSettings.Validate(t
                => !File.Exists($"{Path.Combine(t.PathDirectory, t.NamePhoto)}"),
            t => $"The file named {t.NamePhoto} already exists");
    }

    private static Result<TagCloudSettings> FontExists(TagCloudSettings settings)
    {
        return settings.Validate(t =>
                new InstalledFontCollection().Families.Any(font =>
                    font.Name.Equals(t.Font, StringComparison.OrdinalIgnoreCase)),
            cloudSettings => Errors.Image.FontNotExists(cloudSettings.Font));
    }
}