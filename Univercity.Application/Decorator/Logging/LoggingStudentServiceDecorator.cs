using University.Application.DecoratorBase;
using University.Application.Interfaces;
using University.Application.Responses;
using University.Domain.Entities;

namespace University.Application.Decorator.Logging
{
    public class LoggingStudentServiceDecorator : StudentsDecorator
    {
        public LoggingStudentServiceDecorator(IStudentsInterface inner)
            : base(inner)
        {}
        public override async Task<Response> AddAsync(Students entity)
        {
            try
            {
                LogException.LogToFile($"[INFO] Adding student with ID: {entity.StudentId} and Name: {entity.FullName}");
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
                LogException.LogToFile($"[INFO] Deleting student with ID: {id}");
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

        public override async Task<IEnumerable<Students>> GetAllAsync()
        {
            try
            {
                LogException.LogToFile("[INFO] Retrieving all students");
                var students = await base.GetAllAsync();
                LogException.LogToFile($"[INFO] Retrieved {students.Count()} students");
                LogException.LogToFile($"----------------------------------------------------------");
                return students;
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                throw;
            }
        }

        public override async Task<Students?> GetByIdAsync(int id)
        {
            try
            {
                LogException.LogToFile($"[INFO] Retrieving student with ID: {id}");
                var student = await base.GetByIdAsync(id);
                LogException.LogToFile(student != null
                    ? $"[INFO] Retrieved student with ID: {id}"
                    : $"[INFO] No student found with ID: {id}");
                LogException.LogToFile($"----------------------------------------------------------");
                return student;
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                throw;
            }
        }

        public override async Task<IEnumerable<Students>> GetStudentsByMajorIdAsync(int majorId)
        {
            try
            {
                LogException.LogToFile($"[INFO] Retrieving students for Major ID: {majorId}");
                var students = await base.GetStudentsByMajorIdAsync(majorId);
                LogException.LogToFile($"[INFO] Retrieved {students.Count()} students for Major ID: {majorId}");
                LogException.LogToFile($"----------------------------------------------------------");
                return students;
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                throw;
            }
        }

        public override async Task<Students?> GetStudentWithMajorByIdAsync(int id)
        {
            try
            {
                LogException.LogToFile($"[INFO] Retrieving student with Major for ID: {id}");
                var student = await base.GetStudentWithMajorByIdAsync(id);
                LogException.LogToFile(student != null
                    ? $"[INFO] Retrieved student with Major for ID: {id}"
                    : $"[INFO] No student found with Major for ID: {id}");
                LogException.LogToFile($"----------------------------------------------------------");
                return student;
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                throw;
            }
        }

        public override async Task<bool> StudentExistsAsync(int id)
        {
            try
            {
                LogException.LogToFile($"[INFO] Checking if student exists for ID: {id}");
                var exists = await base.StudentExistsAsync(id);
                LogException.LogToFile($"[INFO] Student exists check for ID {id}: {exists}");
                LogException.LogToFile($"----------------------------------------------------------");
                return exists;
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                throw;
            }
        }

        public override async Task<IEnumerable<Students>> GetStudentsByNameAsync(string name)
        {
            try
            {
                LogException.LogToFile($"[INFO] Retrieving students by name: {name}");
                var students = await base.GetStudentsByNameAsync(name);
                LogException.LogToFile($"[INFO] Retrieved {students.Count()} students for name: {name}");
                LogException.LogToFile($"----------------------------------------------------------");
                return students;
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                throw;
            }
        }

        public override async Task<Response> UpdateAsync(Students entity)
        {
            try
            {
                LogException.LogToFile($"[INFO] Updating student with ID: {entity.StudentId} and Name: {entity.FullName}");
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
