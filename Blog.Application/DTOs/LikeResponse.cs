namespace Blog.Application.DTOs;

public sealed class LikeResponse
{
    public Guid ArticleId { get; init; }
    public string ClientId { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; }
}
