namespace University.Application.DTOs
{
    public record SubjectDto(int SubjectId, string SubjectName, string? Description, int MajorId, int TeacherId);
    
}
