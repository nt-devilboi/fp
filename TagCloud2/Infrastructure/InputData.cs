namespace TagCloud2.Infrastructure;

public class InputData(string[] args)
{
    public virtual string[] GetArgs() => args; // иначе нужно менять тесты
}