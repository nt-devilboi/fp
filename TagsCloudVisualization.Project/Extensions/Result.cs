namespace TagsCloudVisualization.Extensions;

public static class ResultExtension
{
    public static Result<T> Validate<T>(this T obj, Func<T, bool> predicate, Func<T, string> error)
    {
        return predicate(obj) ? Result.Ok(obj) : Result.Fail<T>(error(obj));
    }
}