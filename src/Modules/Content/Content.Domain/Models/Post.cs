using BuildingBlocks.Domain.Abstraction;
using Content.Domain.Events;
using Content.Domain.Models.Enums;
using Content.Domain.ValueObjects;

namespace Content.Domain.Models
{
    public class Post : Aggregate<PostId>
    {
        private readonly List<PostMedia> _media = new();
        public AuthorId AuthorId { get; private set; }
        public PostText Text { get; private set; } = default!;
        public Visibility Visibility { get; private set; } = default!;
        public PostStatus Status { get; private set; }


        public IReadOnlyCollection<PostMedia> Media => _media;

        private Post() { } // EF

        private Post(PostId id, AuthorId authorId, PostText text, Visibility visibility)
        {
            Id = id;
            AuthorId = authorId;
            Text = text;
            Visibility = visibility;
            Status = PostStatus.Published;
            CreatedAt = DateTime.UtcNow;

            Raise(new PostCreatedDomainEvent(Id, AuthorId, CreatedAt.Value));
        }

        public static Post Create(AuthorId authorId, PostText text, Visibility visibility)
            => new(PostId.New(), authorId, text, visibility);

        public void Edit(PostText newText, Visibility newVisibility)
        {
            if (Status == PostStatus.Deleted)
                throw new InvalidOperationException("Cannot edit a deleted post.");

            Text = newText;
            Visibility = newVisibility;
            LastModified = DateTime.UtcNow;

            Raise(new PostEditedDomainEvent(Id, AuthorId, LastModified.Value));
        }

        public void SoftDelete()
        {
            if (Status == PostStatus.Deleted)
                return;

            Status = PostStatus.Deleted;
            LastModified = DateTime.UtcNow;

            Raise(new PostDeletedDomainEvent(Id, AuthorId, LastModified.Value));
        }

        public void AddMedia(string url, MediaType type)
        {
            if (Status == PostStatus.Deleted)
                throw new InvalidOperationException("Cannot add media to a deleted post.");

            _media.Add(PostMedia.Create(url, type, order: _media.Count));
            LastModified = DateTime.UtcNow;
        }

        public void RemoveMedia(PostMediaId mediaId)
        {
            var item = _media.FirstOrDefault(x => x.Id == mediaId);
            if (item is null) return;

            _media.Remove(item);
            LastModified = DateTime.UtcNow;
        }


    }
}
