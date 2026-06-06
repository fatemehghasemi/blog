namespace Blog.Application.Exceptions;

public sealed class InvalidLikeReferenceException : ConflictException
{
    public InvalidLikeReferenceException()
        : base("Like references are invalid for the target article.")
    {
    }

    public InvalidLikeReferenceException(Exception innerException)
        : base("Like references are invalid for the target article.", innerException)
    {
    }
}
