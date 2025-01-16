using System.Drawing;
using TagsCloudVisualization.Result;

namespace TagsCloudVisualization.Abstraction;

public interface ICloudLayouter // так это получается паттерн стратегия
{
    public Point Start { get; }


    public Result<Rectangle> PutNextRectangle(Size rectangleSize);
}