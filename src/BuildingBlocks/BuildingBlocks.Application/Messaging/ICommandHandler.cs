using BuildingBlocks.Domain.Abstraction;
using MediatR;

namespace BuildingBlocks.Application.Messaging;

public interface ICommandHandler<TCommand, TResponse>
    : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>
{
}
