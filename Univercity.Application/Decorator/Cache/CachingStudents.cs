using University.Application.DecoratorBase;
using University.Application.DTOs.ConstantsValues;
using University.Application.Interfaces;
using University.Application.Responses;
using University.Domain.Entities;

namespace University.Application.Decorator.Cache
{
    public class CachingStudents : StudentsDecorator
    {
        private readonly ICacheService _cacheService;
        
        public CachingStudents(IStudentsInterface inner, ICacheService cacheService)
            : base(inner)
        {
            _cacheService = cacheService;
        }

        public override async Task<IEnumerable<Students>> GetAllAsync()
        {
            var cachedStudents = 
                _cacheService.GetData<IEnumerable<Students>>(ConstantsValues.CachingKeys.GetAllStudentsName);

            if (cachedStudents != null)
            {
                return cachedStudents;
            }

            var students = await base.GetAllAsync();
            _cacheService.SetData(ConstantsValues.CachingKeys.GetAllStudentsName, students, DateTimeOffset.Now.AddDays(1));
            return students;
        }

        public override async Task<Students?> GetByIdAsync(int id)
        {
            var cacheKey = $"Student_{id}";
            var cachedStudent = _cacheService.GetData<Students>(cacheKey);

            if (cachedStudent != null)
            {
                return cachedStudent;
            }

            var student = await base.GetByIdAsync(id);
            if (student != null)
            {
                _cacheService.SetData(cacheKey, student, DateTimeOffset.Now.AddDays(1));
            }
            return student;
        }

        public override async Task<Response> AddAsync(Students entity)
        {
            var result = await base.AddAsync(entity);
            if (result.Flag)
            {
                // Clear cache
                _cacheService.RemoveData(ConstantsValues.CachingKeys.GetAllStudentsName);
            }
            return result;
        }

        public override async Task<Response> UpdateAsync(Students entity)
        {
            var result = await base.UpdateAsync(entity);
            if (result.Flag)
            {
                // Clear cache
                _cacheService.RemoveData(ConstantsValues.CachingKeys.GetAllStudentsName);
                _cacheService.RemoveData($"Student_{entity.StudentId}");
            }
            return result;
        }

        public override async Task<Response> DeleteAsync(int id)
        {
            var result = await base.DeleteAsync(id);
            if (result.Flag)
            {
                // Clear cache
                _cacheService.RemoveData(ConstantsValues.CachingKeys.GetAllStudentsName);
                _cacheService.RemoveData($"Student_{id}");
            }
            return result;
        }
    }
}
