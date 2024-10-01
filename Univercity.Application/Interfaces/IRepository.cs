using University.Application.Responses;

namespace University.Application.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<Response> AddAsync(T entity);
        Task<Response> UpdateAsync(T entity);
        Task<Response> DeleteAsync(int id);
    }
}
