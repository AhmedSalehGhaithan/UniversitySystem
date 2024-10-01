using University.Application.Interfaces;
using University.Application.Responses;
using University.Domain.Entities;

namespace University.Application.DecoratorBase
{
    public class StudentsDecorator : IStudentsInterface
    {
        private readonly IStudentsInterface _inner;

        public StudentsDecorator(IStudentsInterface inner)
        {
            _inner = inner;
        }

        public virtual Task<Response> AddAsync(Students entity) 
            => _inner.AddAsync(entity);
        public virtual Task<Response> DeleteAsync(int id) 
            => _inner.DeleteAsync(id);
        public virtual Task<IEnumerable<Students>> GetAllAsync() 
            => _inner.GetAllAsync();
        public virtual Task<Students?> GetByIdAsync(int id) 
            => _inner.GetByIdAsync(id);
        public virtual Task<IEnumerable<Students>> GetStudentsByMajorIdAsync(int majorId) 
            => _inner.GetStudentsByMajorIdAsync(majorId);
        public virtual Task<Students?> GetStudentWithMajorByIdAsync(int id) 
            => _inner.GetStudentWithMajorByIdAsync(id);
        public virtual Task<bool> StudentExistsAsync(int id) 
            => _inner.StudentExistsAsync(id);
        public virtual Task<IEnumerable<Students>> GetStudentsByNameAsync(string name) 
            => _inner.GetStudentsByNameAsync(name);
        public virtual Task<Response> UpdateAsync(Students entity) 
            => _inner.UpdateAsync(entity);
    }
}
