namespace Blog.Application.Likes.Commands.UnlikeArticle;

public sealed class UnlikeArticleCommand
{
    public Guid ArticleId { get; init; }
    public string ClientId { get; init; } = string.Empty;
}
