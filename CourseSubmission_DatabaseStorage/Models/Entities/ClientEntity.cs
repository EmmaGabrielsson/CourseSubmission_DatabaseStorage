using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseSubmission_DatabaseStorage.Models.Entities;

internal class ClientEntity
{
    public Guid Id { get; set; }

    [Column(TypeName = "nvarchar(30)")]
    public string FirstName { get; set; } = null!;

    [Column(TypeName = "nvarchar(60)")]
    public string LastName { get; set; } = null!;

    [Column(TypeName = "nvarchar(100)")]
    public string Email { get; set; } = null!;

    [Column(TypeName = "char(13)")]
    public string? PhoneNumber { get; set; }
    public int AdressId { get; set; }

    public AdressEntity Adress { get; set; } = null!;
    public ICollection<CaseEntity> Cases { get; set; } = new HashSet<CaseEntity>();

}
