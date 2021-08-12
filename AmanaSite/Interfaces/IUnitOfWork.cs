using System.Threading.Tasks;

namespace AmanaSite.Interfaces
{
    public interface IUnitOfWork
    {
        INews News {get;}
        IAds Ads{get;}
        IVideo Video{get;}
        IProject Project{get;}
        IBaladyat Baladyat{get;}
        IInfo Info{get;}
        Idocs Docs{get;}
        Task<bool> Complete();
        bool HasChanges();

        
    }
}