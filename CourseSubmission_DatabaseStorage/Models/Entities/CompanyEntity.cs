using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseSubmission_DatabaseStorage.Models.Entities;

internal class CompanyEntity
{
    public int Id { get; set; }

    [Column(TypeName = "nvarchar(80)")]
    public string CompanyName { get; set; } = null!;
    public ICollection<ServiceWorkerEntity> ServiceWorkers { get; set; } = new HashSet<ServiceWorkerEntity>();
    public ICollection<ClientEntity> Clients { get; set; } = new HashSet<ClientEntity>();

}
