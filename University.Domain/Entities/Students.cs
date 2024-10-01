using System.ComponentModel.DataAnnotations;

namespace University.Domain.Entities
{
    public class Students
    {
        [Key]
        public int StudentId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? IdCard { get; set; }
        public int Age { get; set; }
        public string? PhoneNumber { get; set; }
        public int MajorId { get; set; }
        public virtual Major Major { get; set; } = null!;
    }
}
