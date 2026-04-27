namespace Blog.Application.DTOs;

public sealed class CreateArticleResponse
{
    public Guid Id { get; init; }
    public string Slug { get; init; } = string.Empty;
}
