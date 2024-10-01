using University.Application.DecoratorBase;
using University.Application.DTOs.ConstantsValues;
using University.Application.Interfaces;
using University.Application.Responses;
using University.Domain.Entities;

namespace University.Application.Decorator.Cache
{
    public class CachingMajor : MajorDecorator
    {
        private readonly ICacheService _cacheService;
        public CachingMajor(IMajorInterface inner, ICacheService cacheService)
            : base(inner)
        {
            _cacheService = cacheService;
        }

        public override async Task<IEnumerable<Major>> GetAllAsync()
        {
            var cachedMajors = _cacheService.GetData<IEnumerable<Major>>(ConstantsValues.CachingKeys.GetAllMajorKey);

            if (cachedMajors != null)
            {
                return cachedMajors;
            }

            var majors = await base.GetAllAsync();
            _cacheService.SetData("", majors, DateTimeOffset.Now.AddDays(1));
            return majors;
        }

        public override async Task<Major?> GetByIdAsync(int id)
        {
            var cacheKey = $"Major_{id}";
            var cachedMajor = _cacheService.GetData<Major>(cacheKey);

            if (cachedMajor != null)
            {
                return cachedMajor;
            }

            var major = await base.GetByIdAsync(id);
            if (major != null)
            {
                _cacheService.SetData(cacheKey, major, DateTimeOffset.Now.AddDays(1));
            }
            return major;
        }

        public override async Task<Response> AddAsync(Major entity)
        {
            var result = await base.AddAsync(entity);
            if (result.Flag)
            {
                // Clear cache
                _cacheService.RemoveData(ConstantsValues.CachingKeys.GetAllMajorKey);
            }
            return result;
        }

        public override async Task<Response> UpdateAsync(Major entity)
        {
            var result = await base.UpdateAsync(entity);
            if (result.Flag)
            {
                // Clear cache
                _cacheService.RemoveData(ConstantsValues.CachingKeys.GetAllMajorKey);
                _cacheService.RemoveData($"Major_{entity.MajorId}");
            }
            return result;
        }

        public override async Task<Response> DeleteAsync(int id)
        {
            var result = await base.DeleteAsync(id);
            if (result.Flag)
            {
                // Clear cache
                _cacheService.RemoveData(ConstantsValues.CachingKeys.GetAllMajorKey);
                _cacheService.RemoveData($"Major_{id}");
            }
            return result;
        }
    }
}
