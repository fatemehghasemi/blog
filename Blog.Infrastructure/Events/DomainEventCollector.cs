using Blog.Application.Interfaces;
using Blog.Domain.Common;
using Blog.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Events;

/// <summary>
/// Collects the domain events queued on entities currently tracked by the
/// <see cref="BlogDbContext"/> change tracker, clearing them in the process so
/// they are dispatched exactly once.
/// </summary>
internal sealed class DomainEventCollector : IDomainEventCollector
{
    private readonly BlogDbContext _dbContext;

    public DomainEventCollector(BlogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IReadOnlyCollection<IDomainEvent> CollectAndClear()
    {
        var entities = _dbContext.ChangeTracker
            .Entries<Entity>()
            .Select(entry => entry.Entity)
            .Where(entity => entity.DomainEvents.Count > 0)
            .ToList();

        return entities
            .SelectMany(entity => entity.DequeueDomainEvents())
            .ToList();
    }
}
