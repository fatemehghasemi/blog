namespace Blog.Application.Exceptions;

public sealed class DuplicateLikeException : ConflictException
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
