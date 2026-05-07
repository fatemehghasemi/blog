namespace Blog.Application.Interfaces;

public interface IUnitOfWorkDomainEventProcessor
{
    Task<int> SaveChangesAndDispatchEventsAsync(CancellationToken cancellationToken = default);
}
