using System.ComponentModel.DataAnnotations;

namespace University.Domain.Entities
{
    public class Major
    {
        [Key]
        public int MajorId { get; set; }
        public string MajorName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public virtual ICollection<Students>? Students { get; set; }
        public virtual ICollection<Subject>? Subjects { get; set; }
    }

}
