namespace CourseSubmission_DatabaseStorage.Models.Entities;

internal class CaseCommentEntity
{
    public int Id { get; set; }
    public string TextComment { get; set; } = null!;
    public DateTime Date { get; set; }
    public int ServiceWorkerId { get; set; }
    public ServiceWorkerEntity ServiceWorkerName { get; set; } = null!;
    public int CaseNumber { get; set; }
    public CaseEntity Case { get; set; } = null!;
}
