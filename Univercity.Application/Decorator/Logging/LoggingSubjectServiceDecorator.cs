using University.Application.DecoratorBase;
using University.Application.Interfaces;
using University.Application.Responses;
using University.Domain.Entities;

namespace University.Application.Decorator.Logging
{
    public class LoggingSubjectServiceDecorator : SubjectDecorator
    {
        public LoggingSubjectServiceDecorator(ISubjectInterface inner)
            : base(inner)
        {
        }

        public override async Task<Response> AddAsync(Subject entity)
        {
            try
            {
                LogException.LogToFile($"[INFO] Adding subject with ID: {entity.SubjectId} and Name: {entity.SubjectName}");
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
                LogException.LogToFile($"[INFO] Deleting subject with ID: {id}");
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

        public override async Task<IEnumerable<Subject>> GetAllAsync()
        {
            try
            {
                LogException.LogToFile("[INFO] Retrieving all subjects");
                var subjects = await base.GetAllAsync();
                LogException.LogToFile($"[INFO] Retrieved {subjects.Count()} subjects");
                LogException.LogToFile($"----------------------------------------------------------");
                return subjects;
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                throw;
            }
        }

        public override async Task<Subject?> GetByIdAsync(int id)
        {
            try
            {
                LogException.LogToFile($"[INFO] Retrieving subject with ID: {id}");
                var subject = await base.GetByIdAsync(id);
                LogException.LogToFile(subject != null
                    ? $"[INFO] Retrieved subject with ID: {id}"
                    : $"[INFO] No subject found with ID: {id}");
                LogException.LogToFile($"----------------------------------------------------------");
                return subject;
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                throw; 
            }
        }

        public override async Task<IEnumerable<Subject>> GetSubjectsByMajorIdAsync(int majorId)
        {
            try
            {
                LogException.LogToFile($"[INFO] Retrieving subjects for Major ID: {majorId}");
                var subjects = await base.GetSubjectsByMajorIdAsync(majorId);
                LogException.LogToFile($"[INFO] Retrieved {subjects.Count()} subjects for Major ID: {majorId}");
                LogException.LogToFile($"----------------------------------------------------------");
                return subjects;
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                throw; 
            }
        }

        public override async Task<IEnumerable<Subject>> GetSubjectsByTeacherIdAsync(int teacherId)
        {
            try
            {
                LogException.LogToFile($"[INFO] Retrieving subjects for Teacher ID: {teacherId}");
                var subjects = await base.GetSubjectsByTeacherIdAsync(teacherId);
                LogException.LogToFile($"[INFO] Retrieved {subjects.Count()} subjects for Teacher ID: {teacherId}");
                LogException.LogToFile($"----------------------------------------------------------");
                return subjects;
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                throw; 
            }
        }

        public override async Task<Subject?> GetSubjectWithTeacherByIdAsync(int id)
        {
            try
            {
                LogException.LogToFile($"[INFO] Retrieving subject with Teacher for ID: {id}");
                var subject = await base.GetSubjectWithTeacherByIdAsync(id);
                LogException.LogToFile(subject != null
                    ? $"[INFO] Retrieved subject with Teacher for ID: {id}"
                    : $"[INFO] No subject found with Teacher for ID: {id}");
                LogException.LogToFile($"----------------------------------------------------------");
                return subject;
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                throw; // Re-throwing exception after logging
            }
        }

        public override async Task<bool> SubjectExistsAsync(int id)
        {
            try
            {
                LogException.LogToFile($"[INFO] Checking if subject exists for ID: {id}");
                var exists = await base.SubjectExistsAsync(id);
                LogException.LogToFile($"[INFO] Subject exists check for ID {id}: {exists}");
                LogException.LogToFile($"----------------------------------------------------------");
                return exists;
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                throw; 
            }
        }

        public override async Task<Response> UpdateAsync(Subject entity)
        {
            try
            {
                LogException.LogToFile($"[INFO] Updating subject with ID: {entity.SubjectId} and Name: {entity.SubjectName}");
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
