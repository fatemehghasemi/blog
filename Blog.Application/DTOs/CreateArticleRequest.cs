namespace Blog.Application.DTOs;

public sealed class CreateArticleRequest
{
    public string Title { get; init; } = string.Empty;
    public string Content { get; init; } = string.Empty;
}
