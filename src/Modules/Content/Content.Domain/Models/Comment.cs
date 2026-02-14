using BuildingBlocks.Domain.Abstraction;
using Content.Domain.Models.Enums;
using Content.Domain.ValueObjects;
namespace Content.Domain.Models;

public sealed class Comment : Aggregate<CommentId>
{

    public PostId PostId { get; private set; }
    public AuthorId AuthorId { get; private set; }
    public PostText Text { get; private set; } = default!;
    public CommentStatus Status { get; private set; }
    private Comment() { } // EF

    private Comment(CommentId id, PostId postId, AuthorId authorId, PostText text)
    {
        Id = id;
        PostId = postId;
        AuthorId = authorId;
        Text = text;
        Status = CommentStatus.Active;
        CreatedAt = DateTime.UtcNow;
    }

    public static Comment Create(PostId postId, AuthorId authorId, PostText text)
        => new(CommentId.New(), postId, authorId, text);

    public void Delete()
    {
        if (Status == CommentStatus.Deleted)
            return;

        Status = CommentStatus.Deleted;
        LastModified = DateTime.UtcNow;
    }


    public void Edit(PostText newText)
    {
        if (Status == CommentStatus.Deleted)
            throw new DomainException("Cannot edit a deleted comment.");

        Text = newText;
        LastModified = DateTime.UtcNow;
    }



}
