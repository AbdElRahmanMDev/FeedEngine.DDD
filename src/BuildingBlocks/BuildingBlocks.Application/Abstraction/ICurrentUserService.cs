namespace BuildingBlocks.Application.Abstraction
{
    public interface ICurrentUserService
    {
        Guid? UserId { get; }

    }
}
