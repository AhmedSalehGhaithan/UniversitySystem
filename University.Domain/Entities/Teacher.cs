using System.ComponentModel.DataAnnotations;

namespace University.Domain.Entities
{
    public class Teacher
    {
        [Key]
        public int TeacherId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }
    }
}
