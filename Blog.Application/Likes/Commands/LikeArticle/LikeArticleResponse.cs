namespace Blog.Application.Likes.Commands.LikeArticle;

public sealed class LikeArticleResponse
{
    public Guid ArticleId { get; init; }
    public string ClientId { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; }
}
