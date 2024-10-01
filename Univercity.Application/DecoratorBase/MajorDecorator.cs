using University.Application.Interfaces;
using University.Application.Responses;
using University.Domain.Entities;

namespace University.Application.DecoratorBase
{
    public class MajorDecorator(IMajorInterface _inner) : IMajorInterface
    {
        public virtual Task<Response> AddAsync(Major entity) => _inner.AddAsync(entity);
        public virtual Task<Response> DeleteAsync(int id) => _inner.DeleteAsync(id);
        public virtual Task<IEnumerable<Major>> GetAllAsync() => _inner.GetAllAsync();
        public virtual Task<Major?> GetByIdAsync(int id) => _inner.GetByIdAsync(id);
               
        public virtual Task<IEnumerable<Major>> GetMajorsWithStudentsAsync() => _inner.GetMajorsWithStudentsAsync();
        public virtual Task<IEnumerable<Major>> GetMajorsWithSubjectsAsync() => _inner.GetMajorsWithSubjectsAsync();
               
        public virtual Task<Major?> GetMajorWithStudentsByIdAsync(int id) => _inner.GetMajorWithStudentsByIdAsync(id);
               
        public virtual Task<Major?> GetMajorWithSubjectsByIdAsync(int id) => _inner.GetMajorWithStudentsByIdAsync(id);
               
        public virtual Task<bool> MajorExistsAsync(int id) => _inner.MajorExistsAsync(id);
               
        public virtual Task<Response> UpdateAsync(Major entity) => _inner.UpdateAsync(entity);
    }
}
