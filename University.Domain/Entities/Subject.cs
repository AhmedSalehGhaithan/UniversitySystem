using System.ComponentModel.DataAnnotations;

namespace University.Domain.Entities
{
    public class Subject
    {
        [Key]
        public int SubjectId { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int MajorId { get; set; }
        public virtual Major Major { get; set; } = null!;
        public int TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; } = null!;
    }
}
