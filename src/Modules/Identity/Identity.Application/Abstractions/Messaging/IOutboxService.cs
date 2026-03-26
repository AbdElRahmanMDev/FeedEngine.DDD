using MediatR;

namespace Identity.Application.Abstractions.Messaging
{
    public interface IOutboxService
    {
        Task AddAsync(INotification integrationEvent, CancellationToken cancellationToken);
        Task<int> SavechangeAsync(CancellationToken cancellationToken = default!);

    }
}
