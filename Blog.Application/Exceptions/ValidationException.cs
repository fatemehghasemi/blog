namespace Blog.Application.Exceptions;

/// <summary>
/// Raised when an incoming command fails validation. Carries per-field error
/// messages so the API layer can surface them without depending on any
/// specific validation library.
/// </summary>
public sealed class ValidationException : Exception
{
    public ValidationException(IReadOnlyDictionary<string, string[]> errors)
        : base("One or more validation errors occurred.")
    {
        Errors = errors;
    }

    public IReadOnlyDictionary<string, string[]> Errors { get; }
}
