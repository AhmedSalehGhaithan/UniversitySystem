
using University.Domain.Entities;

namespace University.Application.DTOs.Conventions
{
    public static class MajorConventions
    {
        public static Major ToEntity(MajorDto major) => new()
        {
            MajorId = major.MajorId,
            MajorName = major.MajorNamem,
            Description = major.Description
            
        };
        public static (MajorDto?, IEnumerable<MajorDto>?) FromEntity(Major major, IEnumerable<Major>? majors)
        {
            // return single
            if (major is not null || majors is null)
            {
                var singleMajor = new MajorDto(major!.MajorId, major.MajorName!, major.Description);
                return (singleMajor, null);
            }

            // return list
            if (majors is not null || major is null)
            {
                var _majors = majors!.Select(m => new MajorDto(m!.MajorId, m.MajorName!, m.Description)).ToList();
                return (null, _majors);
            }
            else
                return (null, null);
        }
    }
}
