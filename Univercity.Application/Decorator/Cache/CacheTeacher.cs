using University.Application.DecoratorBase;
using University.Application.Interfaces;
using University.Application.Responses;
using University.Domain.Entities;
using System.Collections.Generic;
using System;
using University.Application.DTOs.ConstantsValues;

namespace University.Application.Decorator.Cache
{
    public class CachingTeacher : TeacherDecorator
    {
        private readonly ICacheService _cacheService;
       
        public CachingTeacher(ITeacherInterface inner, ICacheService cacheService)
            : base(inner)
        {
            _cacheService = cacheService;
        }

        public override async Task<IEnumerable<Teacher>> GetAllAsync()
        {
            var cachedTeachers = 
                _cacheService.GetData<IEnumerable<Teacher>>(ConstantsValues.CachingKeys.GetAllTeachersKey);

            if (cachedTeachers != null)
            {
                return cachedTeachers;
            }

            var teachers = await base.GetAllAsync();
            _cacheService.SetData(ConstantsValues.CachingKeys.GetAllTeachersKey, teachers, DateTimeOffset.Now.AddDays(1));
            return teachers;
        }

        public override async Task<Teacher?> GetByIdAsync(int id)
        {
            var cacheKey = $"Teacher_{id}";
            var cachedTeacher = _cacheService.GetData<Teacher>(cacheKey);

            if (cachedTeacher != null)
            {
                return cachedTeacher;
            }

            var teacher = await base.GetByIdAsync(id);
            if (teacher != null)
            {
                _cacheService.SetData(cacheKey, teacher, DateTimeOffset.Now.AddDays(1));
            }
            return teacher;
        }

        public override async Task<Response> AddAsync(Teacher entity)
        {
            var result = await base.AddAsync(entity);
            if (result.Flag)
            {
                // Clear cache
                _cacheService.RemoveData(ConstantsValues.CachingKeys.GetAllTeachersKey);
            }
            return result;
        }

        public override async Task<Response> UpdateAsync(Teacher entity)
        {
            var result = await base.UpdateAsync(entity);
            if (result.Flag)
            {
                // Clear cache
                _cacheService.RemoveData(ConstantsValues.CachingKeys.GetAllTeachersKey);
                _cacheService.RemoveData($"Teacher_{entity.TeacherId}");
            }
            return result;
        }

        public override async Task<Response> DeleteAsync(int id)
        {
            var result = await base.DeleteAsync(id);
            if (result.Flag)
            {
                // Clear cache
                _cacheService.RemoveData(ConstantsValues.CachingKeys.GetAllTeachersKey);
                _cacheService.RemoveData($"Teacher_{id}");
            }
            return result;
        }

        public override async Task<IEnumerable<Teacher>> GetTeachersByNameAsync(string name)
        {
            var cacheKey = $"TeachersByName_{name}";
            var cachedTeachers = _cacheService.GetData<IEnumerable<Teacher>>(cacheKey);

            if (cachedTeachers != null)
            {
                return cachedTeachers;
            }

            var teachers = await base.GetTeachersByNameAsync(name);
            _cacheService.SetData(cacheKey, teachers, DateTimeOffset.Now.AddDays(1));
            return teachers;
        }

        public override async Task<IEnumerable<Teacher>> GetTeachersWithSubjectsAsync()
        {
            var cachedTeachers = _cacheService.GetData<IEnumerable<Teacher>>(ConstantsValues.CachingKeys.GetAllTeachersKey);

            if (cachedTeachers != null)
            {
                return cachedTeachers;
            }

            var teachers = await base.GetTeachersWithSubjectsAsync();
            _cacheService.SetData(ConstantsValues.CachingKeys.GetAllTeachersKey, teachers, DateTimeOffset.Now.AddDays(1));
            return teachers;
        }

        public override async Task<Teacher?> GetTeacherWithSubjectsByIdAsync(int id)
        {
            var cacheKey = $"TeacherWithSubjects_{id}";
            var cachedTeacher = _cacheService.GetData<Teacher>(cacheKey);

            if (cachedTeacher != null)
            {
                return cachedTeacher;
            }

            var teacher = await base.GetTeacherWithSubjectsByIdAsync(id);
            if (teacher != null)
            {
                _cacheService.SetData(cacheKey, teacher, DateTimeOffset.Now.AddDays(1));
            }
            return teacher;
        }
    }
}
