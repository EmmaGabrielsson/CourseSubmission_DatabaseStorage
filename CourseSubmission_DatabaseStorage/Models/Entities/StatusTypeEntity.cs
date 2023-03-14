using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseSubmission_DatabaseStorage.Models.Entities;

internal class StatusTypeEntity
{
    public int Id { get; set; }

    [Column(TypeName = "nvarchar(30)")]
    public string StatusName { get; set; } = null!;

    public ICollection<CaseEntity> Cases { get; set; } = new HashSet<CaseEntity>();

}
