using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Domain.Entities;

namespace University.Application.DTOs.Conventions
{
    public static class StudentConventions
    {
        public static Students ToEntity(StudentDto student) => new()
        {
            StudentId = student.StudentId,
            FullName = student.FullName,
            IdCard = student.IdCard,
            Age = student.Age,
            PhoneNumber = student.PhoneNumber,
            MajorId = student.MajorId,

        };
        public static (StudentDto?, IEnumerable<StudentDto>?) FromEntity(Students student, IEnumerable<Students>? students)
        {
            // return single
            if (student is not null || students is null)
            {
                var singleStudent = new StudentDto(student!.StudentId, student.FullName!, student.IdCard,student.Age, student.PhoneNumber, student.MajorId);
                return (singleStudent, null);
            }

            // return list
            if (students is not null || student is null)
            {
                var _student = students!.Select(s => new StudentDto(s!.StudentId, s.FullName!, s.IdCard, s.Age, s.PhoneNumber, s.MajorId)).ToList();
                return (null, _student);
            }
            else
                return (null, null);
        }
    }
}
