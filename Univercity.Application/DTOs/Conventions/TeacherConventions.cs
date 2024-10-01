using University.Domain.Entities;

namespace University.Application.DTOs.Conventions
{
    public static class TeacherConventions
    {
        public static Teacher ToEntity(TeacherDto teacher) => new()
        {
            TeacherId = teacher.TeacherId,
            FullName = teacher.FullName,
            Email = teacher.Email,
            PhoneNumber = teacher.PhoneNumber,
        };
        public static (TeacherDto?, IEnumerable<TeacherDto>?) FromEntity(Teacher teacher, IEnumerable<Teacher>? teachers)
        {
            // return single
            if (teacher is not null || teachers is null)
            {
                var singleTeacher = new TeacherDto(teacher!.TeacherId, teacher.FullName!, teacher.Email, teacher.PhoneNumber);
                return (singleTeacher, null);
            }

            // return list
            if (teachers is not null || teacher is null)
            {
                var _teacher = teachers!.Select(t => new TeacherDto(t!.TeacherId, t.FullName!, t.Email, t.PhoneNumber)).ToList();
                return (null, _teacher);
            }
            else
                return (null, null);
        }
    }
}
