using System.Drawing;

namespace TagsCloudVisualization.Abstraction;

public interface ICloudLayouter // так это получается паттерн стратегия
{
    public Point Start { get; }


    public Result<Rectangle> PutNextRectangle(Size rectangleSize);
}