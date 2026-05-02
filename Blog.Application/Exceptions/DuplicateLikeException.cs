namespace Blog.Application.Exceptions;

public sealed class DuplicateLikeException : Exception
{
    public DuplicateLikeException()
        : base("You have already liked this article.")
    {
    }

    public DuplicateLikeException(Exception innerException)
        : base("You have already liked this article.", innerException)
    {
    }
}
