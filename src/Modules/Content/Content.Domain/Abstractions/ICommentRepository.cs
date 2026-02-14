using Content.Domain.Models;
using Content.Domain.ValueObjects;

namespace Content.Domain.Abstractions;

internal interface ICommentRepository
{
    Task AddAsync(Comment comment, CancellationToken cancellationToken);

    Task<Comment?> GetByIdAsync(CommentId id, CancellationToken cancellationToken);

    Task<IReadOnlyList<Comment>> GetByPostAsync(
        PostId postId,
        DateTime? after,
        int take,
        CancellationToken cancellationToken);

    Task<IReadOnlyList<Comment>> GetRepliesAsync(
        CommentId parentId,
        DateTime? after,
        int take,
        CancellationToken cancellationToken);

    Task<int> CountByPostAsync(PostId postId, CancellationToken cancellationToken);

    Task DeleteAsync(Comment comment, CancellationToken cancellationToken);
}
