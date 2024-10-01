using University.Application.DecoratorBase;
using University.Application.Interfaces;
using University.Application.Responses;
using University.Domain.Entities;

namespace University.Application.Decorator.Logging
{
    public class LoggingTeacherServiceDecorator : TeacherDecorator
    {
        public LoggingTeacherServiceDecorator(ITeacherInterface inner)
            : base(inner)
        {
        }

        public override async Task<Response> AddAsync(Teacher entity)
        {
            try
            {
                LogException.LogToFile($"[INFO] Adding teacher with ID: {entity.TeacherId} and Name: {entity.FullName}");
                var result = await base.AddAsync(entity);
                LogException.LogToFile($"[INFO] Add operation result: {result.Message}");
                LogException.LogToFile($"----------------------------------------------------------");
                return result;
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                throw;
            }
        }

        public override async Task<Response> DeleteAsync(int id)
        {
            try
            {
                LogException.LogToFile($"[INFO] Deleting teacher with ID: {id}");
                var result = await base.DeleteAsync(id);
                LogException.LogToFile($"[INFO] Delete operation result: {result.Message}");
                LogException.LogToFile($"----------------------------------------------------------");
                return result;
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                throw;
            }
        }

        public override async Task<IEnumerable<Teacher>> GetAllAsync()
        {
            try
            {
                LogException.LogToFile("[INFO] Retrieving all teachers");
                var teachers = await base.GetAllAsync();
                LogException.LogToFile($"[INFO] Retrieved {teachers.Count()} teachers");
                LogException.LogToFile($"----------------------------------------------------------");
                return teachers;
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                throw;
            }
        }

        public override async Task<Teacher?> GetByIdAsync(int id)
        {
            try
            {
                LogException.LogToFile($"[INFO] Retrieving teacher with ID: {id}");
                var teacher = await base.GetByIdAsync(id);
                LogException.LogToFile(teacher != null
                    ? $"[INFO] Retrieved teacher with ID: {id}"
                    : $"[INFO] No teacher found with ID: {id}");
                LogException.LogToFile($"----------------------------------------------------------");
                return teacher;
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                throw;
            }
        }

        public override async Task<IEnumerable<Teacher>> GetTeachersByNameAsync(string name)
        {
            try
            {
                LogException.LogToFile($"[INFO] Retrieving teachers by name: {name}");
                var teachers = await base.GetTeachersByNameAsync(name);
                LogException.LogToFile($"[INFO] Retrieved {teachers.Count()} teachers for name: {name}");
                LogException.LogToFile($"----------------------------------------------------------");
                return teachers;
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                throw;
            }
        }

        public override async Task<IEnumerable<Teacher>> GetTeachersWithSubjectsAsync()
        {
            try
            {
                LogException.LogToFile("[INFO] Retrieving teachers with subjects");
                var teachers = await base.GetTeachersWithSubjectsAsync();
                LogException.LogToFile($"[INFO] Retrieved {teachers.Count()} teachers with subjects");
                LogException.LogToFile($"----------------------------------------------------------");
                return teachers;
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                throw;
            }
        }

        public override async Task<Teacher?> GetTeacherWithSubjectsByIdAsync(int id)
        {
            try
            {
                LogException.LogToFile($"[INFO] Retrieving teacher with subjects for ID: {id}");
                var teacher = await base.GetTeacherWithSubjectsByIdAsync(id);
                LogException.LogToFile(teacher != null
                    ? $"[INFO] Retrieved teacher with subjects for ID: {id}"
                    : $"[INFO] No teacher found with subjects for ID: {id}");
                LogException.LogToFile($"----------------------------------------------------------");
                return teacher;
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                throw;
            }
        }

        public override async Task<bool> TeacherExistsAsync(int id)
        {
            try
            {
                LogException.LogToFile($"[INFO] Checking if teacher exists for ID: {id}");
                var exists = await base.TeacherExistsAsync(id);
                LogException.LogToFile($"[INFO] Teacher exists check for ID {id}: {exists}");
                LogException.LogToFile($"----------------------------------------------------------");
                return exists;
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                throw;
            }
        }

        public override async Task<Response> UpdateAsync(Teacher entity)
        {
            try
            {
                LogException.LogToFile($"[INFO] Updating teacher with ID: {entity.TeacherId} and Name: {entity.FullName}");
                var result = await base.UpdateAsync(entity);
                LogException.LogToFile($"[INFO] Update operation result: {result.Message}");
                LogException.LogToFile($"----------------------------------------------------------");
                return result;
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                throw;
            }
        }
    }
}
