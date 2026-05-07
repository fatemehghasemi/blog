using Blog.Application.Interfaces;

namespace Blog.Application.Execution;

internal sealed class CommandExecutionPipeline : ICommandExecutionPipeline
{
    private readonly IUnitOfWorkDomainEventProcessor _unitOfWorkDomainEventProcessor;

    public CommandExecutionPipeline(IUnitOfWorkDomainEventProcessor unitOfWorkDomainEventProcessor)
    {
        _unitOfWorkDomainEventProcessor = unitOfWorkDomainEventProcessor;
    }

    public async Task<TResult> ExecuteAsync<TResult>(
        Func<CancellationToken, Task<TResult>> commandExecution,
        CancellationToken cancellationToken = default)
    {
        var result = await commandExecution(cancellationToken);
        await _unitOfWorkDomainEventProcessor.SaveChangesAndDispatchEventsAsync(cancellationToken);
        return result;
    }

    public async Task ExecuteAsync(
        Func<CancellationToken, Task> commandExecution,
        CancellationToken cancellationToken = default)
    {
        await commandExecution(cancellationToken);
        await _unitOfWorkDomainEventProcessor.SaveChangesAndDispatchEventsAsync(cancellationToken);
    }
}
