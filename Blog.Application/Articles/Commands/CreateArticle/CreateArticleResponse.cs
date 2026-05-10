namespace Blog.Application.Articles.Commands.CreateArticle;

public sealed class CreateArticleResponse
{
    public Guid Id { get; init; }
    public string Slug { get; init; } = string.Empty;
}
