using Blog.Domain.ValueObjects;

namespace Blog.Domain.Entities;

public sealed class Article
{
    public Guid Id { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Content { get; private set; } = string.Empty;
    public Slug Slug { get; private set; } = Slug.Generate("untitled");
    public DateTime CreatedAt { get; private set; }

    private Article()
    {
    }

    public Article(string title, string content)
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
}
