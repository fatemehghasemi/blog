namespace Blog.Application.Likes.Commands.LikeArticle;

public sealed class LikeArticleCommand
{
    public Guid ArticleId { get; init; }
    public string ClientId { get; init; } = string.Empty;
}
