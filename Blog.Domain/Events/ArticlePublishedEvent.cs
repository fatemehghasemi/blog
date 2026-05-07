using Blog.Domain.Common;

namespace Blog.Domain.Events;

public sealed class ArticlePublishedEvent : BaseDomainEvent
{
    public Guid ArticleId { get; }
    public string Title { get; }
    public string Slug { get; }

    public ArticlePublishedEvent(Guid articleId, string title, string slug)
    {
        ArticleId = articleId;
        Title = title;
        Slug = slug;
    }
}
