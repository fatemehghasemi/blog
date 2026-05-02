namespace Blog.Domain.Entities;

public sealed class Like
{
    public Guid Id { get; private set; }
    public Guid ArticleId { get; private set; }
    public string ClientId { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }

    private Like()
    {
    }

    public Like(Guid articleId, string clientId)
    {
        if (articleId == Guid.Empty)
        {
            throw new ArgumentException("ArticleId is required.", nameof(articleId));
        }

        if (string.IsNullOrWhiteSpace(clientId))
        {
            throw new ArgumentException("ClientId is required.", nameof(clientId));
        }

        Id = Guid.NewGuid();
        ArticleId = articleId;
        ClientId = clientId.Trim();
        CreatedAt = DateTime.UtcNow;
    }
}
