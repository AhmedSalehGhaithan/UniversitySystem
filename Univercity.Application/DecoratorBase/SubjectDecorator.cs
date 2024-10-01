using University.Application.Interfaces;
using University.Application.Responses;
using University.Domain.Entities;

namespace University.Application.DecoratorBase
{
    public class SubjectDecorator(ISubjectInterface _inner) : ISubjectInterface
    {
        public virtual Task<Response> AddAsync(Subject entity) 
            => _inner.AddAsync(entity);  

        public virtual Task<Response> DeleteAsync(int id) 
            => _inner.DeleteAsync(id);

        public virtual Task<IEnumerable<Subject>> GetAllAsync() 
            => _inner.GetAllAsync();

        public virtual Task<Subject?> GetByIdAsync(int id) 
            => _inner.GetByIdAsync(id);

        public virtual Task<IEnumerable<Subject>> GetSubjectsByMajorIdAsync(int majorId) 
            => _inner.GetSubjectsByMajorIdAsync(majorId);

        public virtual Task<IEnumerable<Subject>> GetSubjectsByTeacherIdAsync(int teacherId)
            => _inner.GetSubjectsByTeacherIdAsync(teacherId);

        public virtual Task<Subject?> GetSubjectWithTeacherByIdAsync(int id)
            => _inner.GetByIdAsync(id);

        public virtual Task<bool> SubjectExistsAsync(int id)
            => _inner.SubjectExistsAsync(id);

        public virtual Task<Response> UpdateAsync(Subject entity)
            => _inner.UpdateAsync(entity);
    }
}
