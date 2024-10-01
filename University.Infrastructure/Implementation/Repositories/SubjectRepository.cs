using Microsoft.EntityFrameworkCore;
using University.Application.DTOs.ConstantsValues;
using University.Application.Interfaces;
using University.Application.Interfaces.StrategyInterfaces;
using University.Application.Responses;
using University.Domain.Entities;
using University.Infrastructure.Data;

namespace University.Infrastructure.Implementation.Repositories
{
    public class SubjectRepository : ISubjectInterface
    {
        private readonly UniversityDbContext _context;
        private readonly IMessageStrategy _messageStrategy;

        public SubjectRepository(UniversityDbContext context, IMessageStrategy messageStrategy)
        {
            _context = context;
            _messageStrategy = messageStrategy;
        }


        public async Task<IEnumerable<Subject>> GetAllAsync()
            => await _context.Subjects.ToListAsync();
        

        public async Task<Subject?> GetByIdAsync(int id)
            => await _context.Subjects.FindAsync(id);
        

        public async Task<Response> AddAsync(Subject entity)
        {
            await _context.Subjects.AddAsync(entity);
            await _context.SaveChangesAsync();
            return new Response(true, 
                _messageStrategy.GetCreateSuccessMessage(ConstantsValues.ObjectType.Subject));
        }

        public async Task<Response> UpdateAsync(Subject entity)
        {
            _context.Subjects.Update(entity);
            await _context.SaveChangesAsync();

            return new Response(true,
                _messageStrategy.GetUpdateSuccessMessage(ConstantsValues.ObjectType.Subject));
        }

        public async Task<Response> DeleteAsync(int id)
        {
            var subject = await GetByIdAsync(id);
            if (subject != null)
            {
                _context.Set<Subject>().Remove(subject);
                await _context.SaveChangesAsync();
            }
            return new Response(true, 
                _messageStrategy.GetDeleteSuccessMessage(ConstantsValues.ObjectType.Subject));
        }

        public async Task<IEnumerable<Subject>> GetSubjectsByMajorIdAsync(int majorId)
            => await _context.Subjects.Where(s => s.MajorId == majorId).ToListAsync();
        
        public async Task<Subject?> GetSubjectWithTeacherByIdAsync(int id)
            => await _context.Subjects.FirstOrDefaultAsync(s => s.SubjectId == id);
        
        public async Task<bool> SubjectExistsAsync(int id)
            => await _context.Subjects.AnyAsync(s => s.SubjectId == id);

        public async Task<IEnumerable<Subject>> GetSubjectsByTeacherIdAsync(int teacherId)
            => await _context.Subjects.Where(s => s.TeacherId == teacherId).ToListAsync();
        
    }
}
