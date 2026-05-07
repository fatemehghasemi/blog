using Blog.Domain.Common;

namespace Blog.Domain.Events;

public sealed class ArticleLikedEvent : BaseDomainEvent
{
    public Guid ArticleId { get; }
    public string ClientId { get; }

    public ArticleLikedEvent(Guid articleId, string clientId)
    {
        ArticleId = articleId;
        ClientId = clientId;
    }
}
