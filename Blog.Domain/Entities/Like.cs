using Blog.Domain.Common;
using Blog.Domain.Events;

namespace Blog.Domain.Entities;

public sealed class Like : Entity
{
    public Guid ArticleId { get; private set; }
    public string ClientId { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }

    private Like()
    {
    }

    private Like(Guid articleId, string clientId)
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

    public static Like Create(Guid articleId, string clientId)
    {
        var like = new Like(articleId, clientId);
        like.AddDomainEvent(new ArticleLikedEvent(like.ArticleId, like.ClientId));
        return like;
    }
}
