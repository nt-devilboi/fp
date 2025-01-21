using System.Drawing;
using FluentAssertions;
using TagsCloudVisualization.Settings;
using TagsCloudVisualization.Test.Extension;

namespace TagsCloudVisualization.Tests;

public class LayouterTests
{
    [Test]
    public void CircularCloudLayouter_Rectangles_ShouldBeHave_Neighbor()
    {
        var setting = new TagCloudSettings
        {
            Size = new Size(500, 500)
        };
        var layoter = new CircularCloudLayouter(setting);

        CheckAllRectanglesHasNeighbor(layoter);
    }

    [TestCase(30, 30, 300, 30)]
    [TestCase(30, 30, 30, 300)]
    [TestCase(1, 1, 30, 300)]
    [TestCase(1, 1, 1, 1)]
    public void CircularCloudLayouter_Rectangle_ShouldBe_Sealing(int widthRec1, int heightRec1, int widthRec2,
        int heightRec2)
    {
        var setting = new TagCloudSettings
        {
            Size = new Size(500, 500)
        };


        var layoter = new CircularCloudLayouter(setting);
        var list = new List<Rectangle>();
        list.Add(layoter.PutNextRectangle(new Size(widthRec1, heightRec1)).GetValueOrThrow());

        var rec = layoter.PutNextRectangle(new Size(widthRec2, heightRec2)).GetValueOrThrow();


        rec.HasNeighbor(list).Should().BeTrue();
    }

    private void CheckAllRectanglesHasNeighbor(CircularCloudLayouter layoter)
    {
        var list = new List<Rectangle>();
        for (var i = 0; i < 30; i++)
        {
            var cur = layoter.PutNextRectangle(new Size(30, 30)).GetValueOrThrow();
            if (list.Count != 0) cur.HasNeighbor(list).Should().BeTrue();

            list.Add(cur);
        }
    }
}