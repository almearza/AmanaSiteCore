using System.Threading.Tasks;

namespace AmanaSite.Interfaces
{
    public interface IUnitOfWork
    {
        INews News {get;}
        Task<bool> Complete();
        bool HasChanges();
    }
}