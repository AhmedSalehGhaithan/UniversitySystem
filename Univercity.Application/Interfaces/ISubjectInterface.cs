using University.Domain.Entities;

namespace University.Application.Interfaces
{
    public interface ISubjectInterface : IRepository<Subject>
    {
        Task<IEnumerable<Subject>> GetSubjectsByMajorIdAsync(int majorId);
        Task<Subject?> GetSubjectWithTeacherByIdAsync(int id);
        Task<bool> SubjectExistsAsync(int id);
        Task<IEnumerable<Subject>> GetSubjectsByTeacherIdAsync(int teacherId);
    }
}
