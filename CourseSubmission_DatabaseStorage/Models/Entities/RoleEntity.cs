using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseSubmission_DatabaseStorage.Models.Entities;

internal class RoleEntity
{
    public int Id { get; set; }

    [Column(TypeName = "nvarchar(80)")]
    public string RoleName { get; set; } = null!;

    public ICollection<EmployeeEntity> Employees { get; set; } = new HashSet<EmployeeEntity>();

}
