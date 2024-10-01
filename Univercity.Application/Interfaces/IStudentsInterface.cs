using University.Domain.Entities;

namespace University.Application.Interfaces
{
    public interface IStudentsInterface : IRepository<Students>
    {
        Task<IEnumerable<Students>> GetStudentsByMajorIdAsync(int majorId);
        Task<Students?> GetStudentWithMajorByIdAsync(int id);
        Task<bool> StudentExistsAsync(int id);
        Task<IEnumerable<Students>> GetStudentsByNameAsync(string name);
    }
}
