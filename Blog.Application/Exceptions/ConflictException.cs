namespace Blog.Application.Exceptions;

/// <summary>
/// Base type for application errors that represent a conflict with the current
/// state of a resource and should surface as HTTP 409.
/// </summary>
public abstract class ConflictException : Exception
{
    protected ConflictException(string message)
        : base(message)
    {
    }

    protected ConflictException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
