using University.Domain.Entities;

namespace University.Application.Interfaces
{
    public interface ITeacherInterface : IRepository<Teacher>
    {
        Task<IEnumerable<Teacher>> GetTeachersWithSubjectsAsync();
        Task<Teacher?> GetTeacherWithSubjectsByIdAsync(int id);
        Task<bool> TeacherExistsAsync(int id);
        Task<IEnumerable<Teacher>> GetTeachersByNameAsync(string name);
    }
}
