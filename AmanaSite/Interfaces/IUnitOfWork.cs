using System.Threading.Tasks;

namespace AmanaSite.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> Complete();
        bool HasChanges();
    }
}