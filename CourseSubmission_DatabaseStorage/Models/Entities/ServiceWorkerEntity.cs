using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseSubmission_DatabaseStorage.Models.Entities;

internal class ServiceWorkerEntity
{
    public int Id { get; set; }

    [Column(TypeName = "nvarchar(30)")]
    public string FirstName { get; set; } = null!;

    [Column(TypeName = "nvarchar(60)")]
    public string LastName { get; set; } = null!;
    public int CompanyId { get; set; }
    public CompanyEntity Company { get; set; } = null!;

}
