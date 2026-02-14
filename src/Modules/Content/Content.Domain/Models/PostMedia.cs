using BuildingBlocks.Domain.Abstraction;
using Content.Domain.Models.Enums;
using Content.Domain.ValueObjects;


namespace Content.Domain.Models;

public class PostMedia : Entity<PostMediaId>
{
    public string Url { get; private set; } = default!;
    public MediaType Type { get; private set; }
    public int Order { get; private set; }

    private PostMedia() { } // EF

    private PostMedia(PostMediaId id, string url, MediaType type, int order)
    {
        Id = id;
        Url = url;
        Type = type;
        Order = order;
    }

    public static PostMedia Create(string url, MediaType type, int order)
        => new(PostMediaId.New(), url, type, order);

}
