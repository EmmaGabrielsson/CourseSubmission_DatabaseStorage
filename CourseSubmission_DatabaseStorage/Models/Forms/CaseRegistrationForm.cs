namespace CourseSubmission_DatabaseStorage.Models.Forms;

internal class CaseRegistrationForm
{
    public CaseRegistrationForm()
    {
        CaseId = new Guid();
        RegistrationDate = DateTime.Now;
        CaseStatusType = "Not Started";
    }
    public Guid CaseId { get; set; }
    public string ClientEmail { get; set; } = null!;
    public string CaseTitle { get; set; } = null!;
    public string? CaseDescription { get; set; }
    public DateTime RegistrationDate { get; set; }
    public string CaseStatusType { get; set; }
}
