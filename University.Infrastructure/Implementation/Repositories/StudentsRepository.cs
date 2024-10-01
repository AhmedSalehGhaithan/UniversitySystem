using Microsoft.EntityFrameworkCore;
using University.Application.DTOs.ConstantsValues;
using University.Application.Interfaces;
using University.Application.Interfaces.StrategyInterfaces;
using University.Application.Responses;
using University.Domain.Entities;
using University.Infrastructure.Data;

namespace University.Infrastructure.Implementation.Repositories
{
    public class StudentsRepository : IStudentsInterface
    {
        private readonly UniversityDbContext _context;
        private readonly IMessageStrategy _messageStrategy;

        public StudentsRepository(UniversityDbContext context, IMessageStrategy messageStrategy)
        {
            _context = context;
            _messageStrategy = messageStrategy;
        }


        public async Task<IEnumerable<Students>> GetAllAsync()
            =>await _context.Students.ToListAsync();
        

        public async Task<Students?> GetByIdAsync(int id)
            =>await _context.Students.FindAsync(id);
        

        public async Task<Response> AddAsync(Students entity)
        {
            await _context.Students.AddAsync(entity);
            await _context.SaveChangesAsync();

            return new Response(true, 
                _messageStrategy.GetCreateSuccessMessage(ConstantsValues.ObjectType.Student));
        }

        public async Task<Response> UpdateAsync(Students entity)
        {
            _context.Students.Update(entity);
            await _context.SaveChangesAsync();

            return new Response(true,
                _messageStrategy.GetUpdateSuccessMessage(ConstantsValues.ObjectType.Student));
        }

        public async Task<Response> DeleteAsync(int id)
        {
            var student = await GetByIdAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
            return new Response(true,
                _messageStrategy.GetDeleteSuccessMessage(ConstantsValues.ObjectType.Student));
        }

        public async Task<IEnumerable<Students>> GetStudentsByMajorIdAsync(int majorId)
            => await _context.Students.Where(s => s.MajorId == majorId).ToListAsync();
        

        public async Task<Students?> GetStudentWithMajorByIdAsync(int id)
            => await _context.Students.Include(s => s.Major).FirstOrDefaultAsync(s => s.StudentId == id);
        

        public async Task<bool> StudentExistsAsync(int id)
            => await _context.Students.AnyAsync(s => s.StudentId == id);
        

        public async Task<IEnumerable<Students>> GetStudentsByNameAsync(string name)
            => await _context.Students.Where(s => s.FullName.Contains(name)).ToListAsync();
        
    }
}
