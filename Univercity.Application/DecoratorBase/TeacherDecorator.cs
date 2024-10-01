using University.Application.Interfaces;
using University.Application.Responses;
using University.Domain.Entities;

namespace University.Application.DecoratorBase
{
    public class TeacherDecorator(ITeacherInterface _inner) : ITeacherInterface
    {
        public virtual Task<Response> AddAsync(Teacher entity)
            => _inner.AddAsync(entity);

        public virtual Task<Response> DeleteAsync(int id)
            => _inner.DeleteAsync(id);

        public virtual Task<IEnumerable<Teacher>> GetAllAsync()
            => _inner.GetAllAsync();

        public virtual Task<Teacher?> GetByIdAsync(int id)
            => _inner.GetByIdAsync(id);

        public virtual Task<IEnumerable<Teacher>> GetTeachersByNameAsync(string name)
            => _inner.GetTeachersByNameAsync(name);

        public virtual Task<IEnumerable<Teacher>> GetTeachersWithSubjectsAsync()
            => _inner.GetTeachersWithSubjectsAsync();

        public virtual Task<Teacher?> GetTeacherWithSubjectsByIdAsync(int id)
            => _inner.GetTeacherWithSubjectsByIdAsync(id);

        public virtual Task<bool> TeacherExistsAsync(int id)
            => _inner.TeacherExistsAsync(id);

        public virtual Task<Response> UpdateAsync(Teacher entity)
            => _inner.UpdateAsync(entity);
    }
}
