using Microsoft.EntityFrameworkCore;
using University.Application.DTOs.ConstantsValues;
using University.Application.Interfaces;
using University.Application.Interfaces.StrategyInterfaces;
using University.Application.Responses;
using University.Domain.Entities;
using University.Infrastructure.Data;

namespace University.Infrastructure.Implementation.Repositories
{
    public class MajorRepository : IMajorInterface
    {
        private readonly UniversityDbContext _context;
        private readonly IMessageStrategy _messageStrategy;

        public MajorRepository(UniversityDbContext context, IMessageStrategy messageStrategy)
        {
            _context = context;
            _messageStrategy = messageStrategy;
        }
        

        public async Task<IEnumerable<Major>> GetAllAsync()
            => await _context.Majors.ToListAsync();
        

        public async Task<Major?> GetByIdAsync(int id)
            => await _context.Majors.FindAsync(id);
        
        public async Task<Response> AddAsync(Major entity)
        {
            await _context.Majors.AddAsync(entity);
            await _context.SaveChangesAsync();
            return new Response(true,
                _messageStrategy.GetCreateSuccessMessage(ConstantsValues.ObjectType.Major));
        }

        public async Task<Response> UpdateAsync(Major entity)
        {
            _context.Majors.Update(entity);
            await _context.SaveChangesAsync();
            return new Response(true, 
                _messageStrategy.GetDeleteSuccessMessage(ConstantsValues.ObjectType.Major));
        }

        public async Task<Response> DeleteAsync(int id)
        {
            var major = await GetByIdAsync(id);
            if (major != null)
            {
                _context.Majors.Remove(major);
                await _context.SaveChangesAsync();
            }
            return new Response(true,
                _messageStrategy.GetUpdateSuccessMessage(ConstantsValues.ObjectType.Major));
        }

        public async Task<IEnumerable<Major>> GetMajorsWithStudentsAsync()
            =>await _context.Majors.Include(m => m.Students).ToListAsync();
        

        public async Task<IEnumerable<Major>> GetMajorsWithSubjectsAsync()
            => await _context.Majors.Include(m => m.Subjects).ToListAsync();
        

        public async Task<Major?> GetMajorWithStudentsByIdAsync(int id)
            => await _context.Majors.Include(m => m.Students).FirstOrDefaultAsync(m => m.MajorId == id);
        

        public async Task<Major?> GetMajorWithSubjectsByIdAsync(int id)
            =>await _context.Majors.Include(m => m.Subjects).FirstOrDefaultAsync(m => m.MajorId == id);
        

        public async Task<bool> MajorExistsAsync(int id)
            => await _context.Majors.AnyAsync(m => m.MajorId == id);
        
    }
}
