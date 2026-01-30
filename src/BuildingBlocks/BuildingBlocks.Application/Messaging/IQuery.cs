using BuildingBlocks.Domain.Abstraction;
using MediatR;


namespace BuildingBlocks.Application.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
