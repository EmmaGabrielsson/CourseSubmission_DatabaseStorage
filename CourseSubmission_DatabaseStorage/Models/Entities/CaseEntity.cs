using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseSubmission_DatabaseStorage.Models.Entities;

internal class CaseEntity
{
    public Guid Id { get; set; }

    [Column(TypeName = "nvarchar(60)")]
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime RegistrationDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public int StatusTypeId { get; set; }
    public Guid ClientId { get; set; }

    public StatusTypeEntity StatusType { get; set; } = null!;
    public ClientEntity Client { get; set; } = null!;
    public ICollection<CommentEntity> Comments { get; set; } = new HashSet<CommentEntity>();
}
