namespace University.Application.DTOs
{
    public record StudentDto(int StudentId, string FullName, string? IdCard, int Age, string? PhoneNumber, int MajorId);
   
}
