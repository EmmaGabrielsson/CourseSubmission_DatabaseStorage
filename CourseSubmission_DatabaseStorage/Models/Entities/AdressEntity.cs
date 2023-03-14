using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseSubmission_DatabaseStorage.Models.Entities;

internal class AdressEntity
{
    public int Id { get; set; }

    [Column(TypeName = "nvarchar(80)")]
    public string StreetName { get; set; } = null!;

    [Column(TypeName = "char(5)")]
    public string PostalCode { get; set; } = null!;

    [Column(TypeName = "nvarchar(80)")]
    public string City { get; set; } = null!;

}
