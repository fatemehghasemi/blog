using Blog.Application.Interfaces;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ValidationException = Blog.Application.Exceptions.ValidationException;

namespace Blog.Application.Execution;

internal sealed class CommandExecutionPipeline : ICommandExecutionPipeline
{
    private readonly IUnitOfWorkDomainEventProcessor _unitOfWorkDomainEventProcessor;
    private readonly IServiceProvider _serviceProvider;

    public CommandExecutionPipeline(
        IUnitOfWorkDomainEventProcessor unitOfWorkDomainEventProcessor,
        IServiceProvider serviceProvider)
    {
        _unitOfWorkDomainEventProcessor = unitOfWorkDomainEventProcessor;
        _serviceProvider = serviceProvider;
    }

    public async Task<TResult> ExecuteAsync<TCommand, TResult>(
        TCommand command,
        Func<TCommand, CancellationToken, Task<TResult>> commandExecution,
        CancellationToken cancellationToken = default)
    {
        await ValidateAsync(command, cancellationToken);

        var result = await commandExecution(command, cancellationToken);
        await _unitOfWorkDomainEventProcessor.SaveChangesAndDispatchEventsAsync(cancellationToken);
        return result;
    }

    public async Task ExecuteAsync<TCommand>(
        TCommand command,
        Func<TCommand, CancellationToken, Task> commandExecution,
        CancellationToken cancellationToken = default)
    {
        await ValidateAsync(command, cancellationToken);

        await commandExecution(command, cancellationToken);
        await _unitOfWorkDomainEventProcessor.SaveChangesAndDispatchEventsAsync(cancellationToken);
    }

    private async Task ValidateAsync<TCommand>(TCommand command, CancellationToken cancellationToken)
    {
        var validators = _serviceProvider.GetServices<IValidator<TCommand>>();

        var failures = new List<FluentValidation.Results.ValidationFailure>();
        foreach (var validator in validators)
        {
            var result = await validator.ValidateAsync(command, cancellationToken);
            if (!result.IsValid)
            {
                failures.AddRange(result.Errors);
            }
        }

        if (failures.Count == 0)
        {
            return;
        }

        var errors = failures
            .GroupBy(failure => failure.PropertyName)
            .ToDictionary(
                group => group.Key,
                group => group.Select(failure => failure.ErrorMessage).Distinct().ToArray());

        throw new ValidationException(errors);
    }
}
