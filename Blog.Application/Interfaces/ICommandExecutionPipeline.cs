namespace Blog.Application.Interfaces;

public interface ICommandExecutionPipeline
{
    Task<TResult> ExecuteAsync<TResult>(
        Func<CancellationToken, Task<TResult>> commandExecution,
        CancellationToken cancellationToken = default);

    Task ExecuteAsync(
        Func<CancellationToken, Task> commandExecution,
        CancellationToken cancellationToken = default);
}
