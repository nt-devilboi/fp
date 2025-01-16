using System.Runtime.Versioning;
using CommandLine;
using TagCloud2.Abstract;
using TagCloud2.Infrastructure;
using TagCloud2.Options;
using TagsCloudVisualization;
using TagsCloudVisualization.Abstraction;
using TagsCloudVisualization.Result;
using TagsCloudVisualization.Settings;

namespace TagCloud2;

[SupportedOSPlatform("windows")]
internal sealed class TagCloudCli(
    TagCloud tagCloud,
    AppSettings appSettings,
    AbstractFactoryBitMap factoryCloudBitMap,
    ILogger logger,
    InputData consoleData)
    : ITagCloudController
{
    public void Run()
    {
        Result.Of(() => Parser.Default.ParseArguments<CreateTagCloud>(consoleData.GetArgs())
                .WithParsed(CreateCloud))
            .RefineError("Argument invalid")
            .OnFail(logger.WriteLine);
    }

    private void CreateCloud(CreateTagCloud createTagCloud)
    {
        SetParameters(createTagCloud)
            .Then(CreateBitMap)
            .Then(tagCloud.GenerateCloud)
            .Then(SaveCloud)
            .OnFail(logger.WriteLine);
    }

    private static Result<None> SaveCloud(ITagCloudSave tagCloudImage)
    {
        tagCloudImage.Save();
        return Result.Ok();
    }

    private Result<ITagCloudImage> CreateBitMap()
    {
        return factoryCloudBitMap.Create();
    }

    private Result<None> SetParameters(CreateTagCloud createTagCloud)
    {
        var imageFormat = createTagCloud.GetImageFormat().Then(x => appSettings.TagCloudSettings.ImageFormat = x);
        if (!imageFormat.IsSuccess) return Result.Fail<None>(imageFormat.Error);

        appSettings.TagCloudSettings.PathDirectory = createTagCloud.Directory;
        appSettings.TagCloudSettings.Size = createTagCloud.GetSize();
        appSettings.TagCloudSettings.NamePhoto = createTagCloud.NamePhoto;

        appSettings.TagCloudSettings.ColorWords = createTagCloud.Color;
        appSettings.TagCloudSettings.BackGround = createTagCloud.BackgrondColor;
        appSettings.TagCloudSettings.EmSize = createTagCloud.EmSize;

        appSettings.TagCloudSettings.Font = createTagCloud.Font;

        appSettings.WordLoaderSettings.PathTextFile = createTagCloud.PathToWords;

        return Result.Ok();
    }
}