namespace Blog.Application.Exceptions;

public sealed class InvalidCommentReferenceException : Exception
{
    public InvalidCommentReferenceException()
        : base("Comment references are invalid for the target article.")
    {
    }

    public InvalidCommentReferenceException(Exception innerException)
        : base("Comment references are invalid for the target article.", innerException)
    {
    }
}
