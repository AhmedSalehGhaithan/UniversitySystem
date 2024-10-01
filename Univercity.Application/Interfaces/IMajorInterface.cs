using University.Domain.Entities;

namespace University.Application.Interfaces
{
    public interface IMajorInterface : IRepository<Major>
    {
        Task<IEnumerable<Major>> GetMajorsWithStudentsAsync();
        Task<IEnumerable<Major>> GetMajorsWithSubjectsAsync();
        Task<Major?> GetMajorWithStudentsByIdAsync(int id);
        Task<Major?> GetMajorWithSubjectsByIdAsync(int id);
        Task<bool> MajorExistsAsync(int id);
    }
}
