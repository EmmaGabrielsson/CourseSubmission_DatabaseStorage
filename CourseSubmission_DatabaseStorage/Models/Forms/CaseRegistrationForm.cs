namespace CourseSubmission_DatabaseStorage.Models.Forms;

internal class CaseRegistrationForm
{
    public int ClientId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string ClientEmail { get; set; } = null!;
    public string? ClientPhoneNumber { get; set; }
    public string StreetName { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string City { get; set; } = null!;
    public string CaseDescription { get; set; } = null!;
    public DateTime RegistrationDate { get; set; }
    public string CaseStatus { get; set; } = null!;
}
