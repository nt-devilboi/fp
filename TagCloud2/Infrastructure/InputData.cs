namespace TagCloud2.Infrastructure;

public class InputData
{
    public virtual string[] GetArgs()
    {
        return Environment.GetCommandLineArgs();
    }
}