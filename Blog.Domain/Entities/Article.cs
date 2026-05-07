using Blog.Domain.Common;
using Blog.Domain.Events;
using Blog.Domain.ValueObjects;

namespace Blog.Domain.Entities;

public sealed class Article : Entity
{
    public string Title { get; private set; } = string.Empty;
    public string Content { get; private set; } = string.Empty;
    public Slug Slug { get; private set; } = Slug.Generate("untitled");
    public DateTime CreatedAt { get; private set; }

    private Article()
    {
    }

    private Article(string title, string content)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title is required.", nameof(title));
        }

        if (string.IsNullOrWhiteSpace(content))
        {
            throw new ArgumentException("Content is required.", nameof(content));
        }

        Id = Guid.NewGuid();
        Title = title.Trim();
        Content = content.Trim();
        Slug = Slug.Generate(Title);
        CreatedAt = DateTime.UtcNow;
    }

    public static Article Create(string title, string content)
    {
        var article = new Article(title, content);
        article.AddDomainEvent(new ArticlePublishedEvent(article.Id, article.Title, article.Slug.Value));
        return article;
    }
}
