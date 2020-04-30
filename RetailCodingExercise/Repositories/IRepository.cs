using System.Collections.Generic;
using System.Threading.Tasks;

namespace RetailCodingExercise.Repositories
{
    // TODO: If multiple CRUD operation meant to be done, repository pattern would be useful
    public interface IRepository<T>
    {
        Task<List<T>> GetAll();
        Task<T> Get(int id);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(int id);
    }
}
