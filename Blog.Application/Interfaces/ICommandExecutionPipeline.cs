namespace Blog.Application.Interfaces;

public interface ICommandExecutionPipeline
{
    Task<TResult> ExecuteAsync<TCommand, TResult>(
        TCommand command,
        Func<TCommand, CancellationToken, Task<TResult>> commandExecution,
        CancellationToken cancellationToken = default);

    Task ExecuteAsync<TCommand>(
        TCommand command,
        Func<TCommand, CancellationToken, Task> commandExecution,
        CancellationToken cancellationToken = default);
}
