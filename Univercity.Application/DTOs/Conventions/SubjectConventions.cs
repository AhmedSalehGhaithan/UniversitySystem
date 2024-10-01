using University.Domain.Entities;

namespace University.Application.DTOs.Conventions
{
    public static class SubjectConventions
    {
        public static Subject ToEntity(SubjectDto subjectDto) => new()
        {
            SubjectId = subjectDto.SubjectId,
            SubjectName = subjectDto.SubjectName,
            Description = subjectDto.Description,
            MajorId = subjectDto.MajorId,
            TeacherId = subjectDto.TeacherId,
        };
        public static (SubjectDto?, IEnumerable<SubjectDto>?) FromEntity(Subject subject, IEnumerable<Subject>? subjects)
        {
            // return single
            if (subject is not null || subjects is null)
            {
                var singleSubject = new SubjectDto(subject!.SubjectId, subject.SubjectName!, subject.Description,subject.MajorId,subject.TeacherId);
                return (singleSubject, null);
            }

            // return list
            if (subjects is not null || subject is null)
            {
                var _subject = subjects!.Select(s => new SubjectDto(s!.SubjectId, s.SubjectName!, s.Description, s.MajorId, s.TeacherId)).ToList();
                return (null, _subject);
            }
            else
                return (null, null);
        }
    }
}
