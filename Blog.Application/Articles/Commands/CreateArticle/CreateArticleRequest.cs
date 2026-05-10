namespace Blog.Application.Articles.Commands.CreateArticle;

public sealed class CreateArticleRequest
{
    public string Title { get; init; } = string.Empty;
    public string Content { get; init; } = string.Empty;
}
