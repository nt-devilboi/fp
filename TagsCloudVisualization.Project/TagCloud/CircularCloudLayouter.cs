using System.Drawing;
using Microsoft.AspNetCore.Http.HttpResults;
using TagsCloudVisualization.Abstraction;
using TagsCloudVisualization.Result;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization;

internal class CircularCloudLayouter : ICloudLayouter
{
    private const double CoefficientRadius = 1.3;
    private const int CoefficientAngle = 14;
    private readonly List<Rectangle> _rectangles;
    private readonly TagCloudSettings _tagCloudSettings;
    private double _angle;

    public CircularCloudLayouter(TagCloudSettings tagCloudSettings)
    {
        Validate(tagCloudSettings.Center);
        _rectangles = [];
        _tagCloudSettings = tagCloudSettings;
    }

    public Point Start => _tagCloudSettings.Center;

    public Result<Rectangle> PutNextRectangle(Size rectangleSize)
    {
        var result = Validate(_tagCloudSettings.Center);
        if (!result.IsSuccess) return Result.Result.Fail<Rectangle>(result.Error);

        Rectangle rec;
        do
        {
            _angle += Math.PI / CoefficientAngle;
            var radius = CoefficientRadius * _angle;
            var x = (int)(Start.X + radius * Math.Cos(_angle));
            var y = (int)(Start.Y + radius * Math.Sin(_angle));
            rec = new Rectangle(new Point(x, y), rectangleSize);
        } while (AnyIntersectWithRec(rec));

        if (_rectangles.Count != 0) rec = Sealing(rec);
        _rectangles.Add(rec);
        return rec;
    }

    private static Result<None> Validate(Point center)
    {
        var result = Result.Result.Fail<None>("");
        if (center.X < 0)
        {
            result.RefineError("X has value less than 0");
        }

        if (center.Y < 0)
        {
            result.RefineError("Y has value less than 0");
        }

        return string.IsNullOrEmpty(result.Error) ? Result.Result.Ok() : result;
    }

    private Rectangle Sealing(Rectangle rec)
    {
        while (Start.Y - rec.Bottom > 1 && !AnyIntersectWithRec(rec with { Y = rec.Y + 2 })) rec.Y += 1;
        while (rec.Top - Start.Y > 1 && !AnyIntersectWithRec(rec with { Y = rec.Y - 2 })) rec.Y -= 1;
        while (rec.Left - Start.X > 1 && !AnyIntersectWithRec(rec with { X = rec.X - 2 })) rec.X -= 1;
        while (Start.X - rec.Right > 1 && !AnyIntersectWithRec(rec with { X = rec.X + 2 })) rec.X += 1;

        return rec;
    }

    private bool AnyIntersectWithRec(Rectangle rec)
    {
        return _rectangles.Any(rectangle => rectangle.IntersectsWith(rec));
    }
}