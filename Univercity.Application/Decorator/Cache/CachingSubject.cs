using University.Application.DecoratorBase;
using University.Application.DTOs.ConstantsValues;
using University.Application.Interfaces;
using University.Application.Responses;
using University.Domain.Entities;

namespace University.Application.Decorator.Cache
{
    public class CachingSubject : SubjectDecorator
    {
        private readonly ICacheService _cacheService;

        public CachingSubject(ISubjectInterface inner, ICacheService cacheService)
            : base(inner)
        {
            _cacheService = cacheService;
        }

        public override async Task<IEnumerable<Subject>> GetAllAsync()
        {
            var cachedSubjects = 
                _cacheService.GetData<IEnumerable<Subject>>(ConstantsValues.CachingKeys.GetAllSubjectKey);

            if (cachedSubjects != null)
            {
                return cachedSubjects;
            }

            var subjects = await base.GetAllAsync();
            _cacheService.SetData(ConstantsValues.CachingKeys.GetAllSubjectKey, subjects, DateTimeOffset.Now.AddDays(1));
            return subjects;
        }

        public override async Task<Subject?> GetByIdAsync(int id)
        {
            var cacheKey = $"Subject_{id}";
            var cachedSubject = _cacheService.GetData<Subject>(cacheKey);

            if (cachedSubject != null)
            {
                return cachedSubject;
            }

            var subject = await base.GetByIdAsync(id);
            if (subject != null)
            {
                _cacheService.SetData(cacheKey, subject, DateTimeOffset.Now.AddDays(1));
            }
            return subject;
        }

        public override async Task<Response> AddAsync(Subject entity)
        {
            var result = await base.AddAsync(entity);
            if (result.Flag)
            {
                // Clear cache
                _cacheService.RemoveData(ConstantsValues.CachingKeys.GetAllSubjectKey);
            }
            return result;
        }

        public override async Task<Response> UpdateAsync(Subject entity)
        {
            var result = await base.UpdateAsync(entity);
            if (result.Flag)
            {
                // Clear cache
                _cacheService.RemoveData(ConstantsValues.CachingKeys.GetAllSubjectKey);
                _cacheService.RemoveData($"Subject_{entity.SubjectId}");
            }
            return result;
        }

        public override async Task<Response> DeleteAsync(int id)
        {
            var result = await base.DeleteAsync(id);
            if (result.Flag)
            {
                // Clear cache
                _cacheService.RemoveData(ConstantsValues.CachingKeys.GetAllSubjectKey);
                _cacheService.RemoveData($"Subject_{id}");
            }
            return result;
        }
    }
}
