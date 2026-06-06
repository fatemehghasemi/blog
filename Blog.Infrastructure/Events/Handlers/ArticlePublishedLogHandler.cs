using Blog.Application.Interfaces;
using Blog.Domain.Events;
using Microsoft.Extensions.Logging;

namespace Blog.Infrastructure.Events.Handlers;

/// <summary>
/// Minimal handler that logs published articles. Serves as the seam where
/// future reactions (e.g. email notifications, search indexing) will hook in.
/// </summary>
internal sealed class ArticlePublishedLogHandler : IDomainEventHandler<ArticlePublishedEvent>
{
    private readonly ILogger<ArticlePublishedLogHandler> _logger;

    public ArticlePublishedLogHandler(ILogger<ArticlePublishedLogHandler> logger)
    {
        _logger = logger;
    }

    public Task HandleAsync(ArticlePublishedEvent domainEvent, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "Article published: {ArticleId} '{Title}' (slug: {Slug})",
            domainEvent.ArticleId, domainEvent.Title, domainEvent.Slug);

        return Task.CompletedTask;
    }
}
