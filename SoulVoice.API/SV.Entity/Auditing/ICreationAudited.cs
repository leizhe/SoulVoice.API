
namespace SV.Entity.Auditing
{
    public interface ICreationAudited : IHasCreationTime
    {
        long? CreatorUserId { get; } 
    }
}