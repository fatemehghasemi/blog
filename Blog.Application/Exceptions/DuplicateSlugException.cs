namespace Blog.Application.Exceptions;

public sealed class DuplicateSlugException : Exception
{
    public DuplicateSlugException(string slug)
        : base($"An article with slug '{slug}' already exists.")
    {
    }

    public DuplicateSlugException(string slug, Exception innerException)
        : base($"An article with slug '{slug}' already exists.", innerException)
    {
    }
}
