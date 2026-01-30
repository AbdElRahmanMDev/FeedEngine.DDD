using BuildingBlocks.Domain.Abstraction;
using MediatR;

namespace BuildingBlocks.Application.Messaging;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{

}
public interface ICommand : IRequest<Result>
{
}
