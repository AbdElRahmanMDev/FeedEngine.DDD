using Content.Domain.Models;
using Content.Domain.ValueObjects;


namespace Content.Domain.Abstractions;

public interface IPostRepository
{
    Task AddAsync(Post post, CancellationToken cancellationToken);

    Task<Post?> GetByIdAsync(PostId id, CancellationToken cancellationToken);

    Task<IReadOnlyList<Post>> GetByAuthorAsync(
        AuthorId authorId,
        DateTime? after,
        int take,
        CancellationToken cancellationToken);

    Task<IReadOnlyList<Post>> GetByAuthorsAsync(
        IReadOnlyCollection<AuthorId> authorIds,
        DateTime? after,
        int take,
        CancellationToken cancellationToken);

    Task DeleteAsync(Post post, CancellationToken cancellationToken);

    Task SaveChangesAsync(CancellationToken cancellationToken);
}
