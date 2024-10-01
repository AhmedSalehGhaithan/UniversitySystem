using University.Application.DecoratorBase;
using University.Application.Interfaces;
using University.Application.Responses;
using University.Domain.Entities;
namespace University.Application.Decorator.Logging
{
    public class LoggingMajorServiceDecorator : MajorDecorator
    {
        public LoggingMajorServiceDecorator(IMajorInterface inner)
            : base(inner)
        {
        }

        public override async Task<Response> AddAsync(Major entity)
        {
            try
            {
                LogException.LogToFile($"[INFO] Adding major with ID: {entity.MajorId}");
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
                LogException.LogToFile($"[INFO] Deleting major with ID: {id}");
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

        public override async Task<IEnumerable<Major>> GetAllAsync()
        {
            try
            {
                LogException.LogToFile("[INFO] Retrieving all majors");
                var majors = await base.GetAllAsync();
                LogException.LogToFile($"[INFO] Retrieved {majors.Count()} majors");
                LogException.LogToFile($"----------------------------------------------------------");
                return majors;
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                throw; 
            }
        }

        public override async Task<Major?> GetByIdAsync(int id)
        {
            try
            {
                LogException.LogToFile($"[INFO] Retrieving major with ID: {id}");
                var major = await base.GetByIdAsync(id);
                LogException.LogToFile(major != null
                    ? $"[INFO] Retrieved major with ID: {id}"
                    : $"[INFO] No major found with ID: {id}");
                LogException.LogToFile($"----------------------------------------------------------");
                return major;
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                throw; 
            }
        }

        public override async Task<IEnumerable<Major>> GetMajorsWithStudentsAsync()
        {
            try
            {
                LogException.LogToFile("[INFO] Retrieving majors with students");
                var majors = await base.GetMajorsWithStudentsAsync();
                LogException.LogToFile($"[INFO] Retrieved {majors.Count()} majors with students");
                LogException.LogToFile($"----------------------------------------------------------");
                LogException.LogToFile($"----------------------------------------------------------");
                return majors;
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                throw;
            }
        }

        public override async Task<IEnumerable<Major>> GetMajorsWithSubjectsAsync()
        {
            try
            {
                LogException.LogToFile("[INFO] Retrieving majors with subjects");
                var majors = await base.GetMajorsWithSubjectsAsync();
                LogException.LogToFile($"[INFO] Retrieved {majors.Count()} majors with subjects");
                LogException.LogToFile($"----------------------------------------------------------");
                return majors;
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                throw; 
            }
        }

        public override async Task<Major?> GetMajorWithStudentsByIdAsync(int id)
        {
            try
            {
                LogException.LogToFile($"[INFO] Retrieving major with students for ID: {id}");
                var major = await base.GetMajorWithStudentsByIdAsync(id);
                LogException.LogToFile(major != null
                    ? $"[INFO] Retrieved major with students for ID: {id}"
                    : $"[INFO] No major found with students for ID: {id}");
                LogException.LogToFile($"----------------------------------------------------------");
                return major;
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                throw; 
            }
        }

        public override async Task<Major?> GetMajorWithSubjectsByIdAsync(int id)
        {
            try
            {
                LogException.LogToFile($"[INFO] Retrieving major with subjects for ID: {id}");
                var major = await base.GetMajorWithSubjectsByIdAsync(id);
                LogException.LogToFile(major != null
                    ? $"[INFO] Retrieved major with subjects for ID: {id}"
                    : $"[INFO] No major found with subjects for ID: {id}");
                LogException.LogToFile($"----------------------------------------------------------");
                return major;
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                throw; 
            }
        }

        public override async Task<bool> MajorExistsAsync(int id)
        {
            try
            {
                LogException.LogToFile($"[INFO] Checking if major exists for ID: {id}");
                var exists = await base.MajorExistsAsync(id);
                LogException.LogToFile($"[INFO] Major exists check for ID {id}: {exists}");
                LogException.LogToFile($"----------------------------------------------------------");
                return exists;
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                throw;
            }
        }

        public override async Task<Response> UpdateAsync(Major entity)
        {
            try
            {
                LogException.LogToFile($"[INFO] Updating major with ID: {entity.MajorId} and Name: {entity.MajorName}");
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
