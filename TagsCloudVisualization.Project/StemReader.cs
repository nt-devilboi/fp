using System.Diagnostics;
using System.Text;
using TagsCloudVisualization.Abstraction;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization;

internal sealed class StemReader : IStemReader
{
    private readonly Process process;
    private readonly StreamReader streamReader;

    public StemReader(WordLoaderSettings wordLoaderSettings)
    {
        var stem = new Process
        {
            StartInfo =
            {
                FileName = "mystem",
                Arguments = $"-nli {wordLoaderSettings.PathTextFile}",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                StandardOutputEncoding = Encoding.UTF8
            }
        };


        process = stem;
        process.Start();
        streamReader = process.StandardOutput;
    }

    public void Dispose()
    {
        process.Close();
    }

    public string? ReadLine()
    {
        return streamReader.ReadLine();
    }

    public IEnumerable<string> ReadLines()
    {
        var line = streamReader.ReadLine();
        while (line != null)
        {
            yield return line.ToLower();
            line = ReadLine();
        }
    }
}