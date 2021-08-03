using System.Threading.Tasks;

namespace AmanaSite.Interfaces
{
    public interface IUnitOfWork
    {
        INews News {get;}
        IAds Ads{get;}
        // IAmanaService AmanaService{get;}
        // IMob Mob{get;}
        IVideo Video{get;}
        Task<bool> Complete();
        bool HasChanges();
    }
}