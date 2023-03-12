using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseSubmission_DatabaseStorage.Models.Entities;

internal class CaseEntity
{
    [Key]
    public int CaseNumber { get; set; }
    public string Description { get; set; } = null!;
    public DateTime RegistrationDate { get; set; }
    public DateTime? CompletedDate { get; set; }

    [Column(TypeName = "nvarchar(30)")]
    public string CaseStatus { get; set; } = null!;
    public int ClientId { get; set; }
    public ClientEntity Client { get; set; } = null!;
    public ICollection<CaseCommentEntity> Comments { get; set; } = new HashSet<CaseCommentEntity>();
}
