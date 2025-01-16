using System.Drawing;
using TagsCloudVisualization.Abstraction;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization;

public class TagCloud(
    ICloudLayouter cloudLayouter,
    IWordLoader wordLoader,
    TagCloudSettings tagCloudSettings,
    ISizeWord sizeWord)
{
    private static Result<ITagCloudImage> Validate(ICloudLayouter cloudLayouter, ITagCloudImage tagCloudImage)
    {
        if (cloudLayouter.Start.Y > tagCloudImage.Size().Height ||
            cloudLayouter.Start.X > tagCloudImage.Size().Width)
            return new Result<ITagCloudImage>("the start position is abroad of image");

        return tagCloudImage.AsResult();
    }

    public Result<ITagCloudSave> GenerateCloud(ITagCloudImage tagCloudImage)
    {
        return Validate(cloudLayouter, tagCloudImage)
            .Then(LoadWord)
            .Then(AddWordInCloud)
            .RefineError(Errors.Cloud.ScopeMessage());
    }

    private Result<ITagCloudSave> AddWordInCloud(
        (ITagCloudImage tagCloudImage, ICollection<FrequencyWord> words) data)
    {
        var emSize = tagCloudSettings.EmSize;
        foreach (var word in data.words)
        {
            var size = sizeWord.GetSizeWord(word.Word, emSize);
            size.Width += 20;

            var createdRec = cloudLayouter.PutNextRectangle(size).Then(CheckRecInImage);
            if (!createdRec.IsSuccess) return Result.Fail<ITagCloudSave>(createdRec.Error);

            var recCloud = new RectangleTagCloud(createdRec.GetValueOrThrow(), word.Word, emSize);
            data.tagCloudImage.DrawString(recCloud);
            emSize = emSize > 14 ? emSize - 1 : 24;
        }

        return ((ITagCloudSave)data.tagCloudImage).AsResult();
    }


    private Result<Rectangle> CheckRecInImage(Rectangle rectangle)
    {
        if (rectangle.Right > tagCloudSettings.Size.Width ||
            rectangle.Bottom > tagCloudSettings.Size.Height ||
            rectangle.Left < 0 ||
            rectangle.Top < 0)
            return Result.Fail<Rectangle>(Errors.Cloud.WordOutsideImage());


        return rectangle.AsResult();
    }


    private Result<(ITagCloudImage, ICollection<FrequencyWord>)> LoadWord(ITagCloudImage tagCloudImage)
    {
        return wordLoader.LoadWords()
            .Then(NotEmpty)
            .Then(Sort)
            .Then(c => (tagCloudImage, c));
    }


    private static ICollection<FrequencyWord> Sort(ICollection<FrequencyWord> words)
    {
        var wordsSort = words.ToList();

        wordsSort.Sort((prev, cur) => cur.Count.CompareTo(prev.Count));
        return wordsSort;
    }

    private static Result<ICollection<FrequencyWord>> NotEmpty(ICollection<FrequencyWord> words)
    {
        if (words.Count == 0)
            return Result.Fail<ICollection<FrequencyWord>>(Errors.Stem.TextIsEmptyOrOnlyBoringWords());

        return Result.Ok(words);
    }
}