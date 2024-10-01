using Microsoft.EntityFrameworkCore;
using University.Application.DTOs.ConstantsValues;
using University.Application.Interfaces;
using University.Application.Interfaces.StrategyInterfaces;
using University.Application.Responses;
using University.Domain.Entities;
using University.Infrastructure.Data;

namespace University.Infrastructure.Implementation.Repositories
{
    public class TeacherRepository : ITeacherInterface
    {
        private readonly UniversityDbContext _context;
        private readonly IMessageStrategy _messageStrategy;

        public TeacherRepository(UniversityDbContext context, IMessageStrategy messageStrategy)
        {
            _context = context;
            _messageStrategy = messageStrategy;
        }

        public async Task<IEnumerable<Teacher>> GetAllAsync()
             => await _context.Set<Teacher>().ToListAsync();
        

        public async Task<Teacher?> GetByIdAsync(int id)
            => await _context.Set<Teacher>().FindAsync(id);
        
        public async Task<Response> AddAsync(Teacher entity)
        {
            await _context.Set<Teacher>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return new Response(true, 
                _messageStrategy.GetCreateSuccessMessage(ConstantsValues.ObjectType.Teacher));
        }

        public async Task<Response> UpdateAsync(Teacher entity)
        {
            _context.Set<Teacher>().Update(entity);
            await _context.SaveChangesAsync();
            return new Response(true,
                _messageStrategy.GetUpdateSuccessMessage(ConstantsValues.ObjectType.Teacher));
        }

        public async Task<Response> DeleteAsync(int id)
        {
            var teacher = await GetByIdAsync(id);
            if (teacher != null)
            {
                _context.Set<Teacher>().Remove(teacher);
                await _context.SaveChangesAsync();
            }
            return new Response(true, 
                _messageStrategy.GetDeleteSuccessMessage(ConstantsValues.ObjectType.Teacher));
        }

        public async Task<IEnumerable<Teacher>> GetTeachersWithSubjectsAsync()
            => await _context.Set<Teacher>().Include(t => t.Subjects).ToListAsync();

        public async Task<Teacher?> GetTeacherWithSubjectsByIdAsync(int id)
            => await _context.Set<Teacher>().Include(t => t.Subjects).FirstOrDefaultAsync(t => t.TeacherId == id);
        
        public async Task<bool> TeacherExistsAsync(int id)
            => await _context.Set<Teacher>().AnyAsync(t => t.TeacherId == id);
        
        public async Task<IEnumerable<Teacher>> GetTeachersByNameAsync(string name)
            => await _context.Set<Teacher>().Where(t => t.FullName.Contains(name)).ToListAsync();
    }
}
