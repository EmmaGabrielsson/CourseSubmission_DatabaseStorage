namespace CourseSubmission_DatabaseStorage.Models.Entities;

internal class CommentEntity
{
    public Guid Id { get; set; }
    public string TextComment { get; set; } = null!;
    public DateTime Created { get; set; }
    public Guid CaseId { get; set; }
    public int EmployeeId { get; set; }

    public CaseEntity Case { get; set; } = null!;
    public EmployeeEntity Employee { get; set; } = null!;
}
