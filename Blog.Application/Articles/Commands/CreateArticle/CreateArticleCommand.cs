namespace Blog.Application.Articles.Commands.CreateArticle;

public sealed class CreateArticleCommand
{
    public string Title { get; init; } = string.Empty;
    public string Content { get; init; } = string.Empty;
}
