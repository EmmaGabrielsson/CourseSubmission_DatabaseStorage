using CourseSubmission_DatabaseStorage.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourseSubmission_DatabaseStorage.Contexts;

internal class DataContext : DbContext
{
    #region Constructors and Overrides
    public DataContext()
    {
    }
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\emmag\source\repos\Datalagring\CourseSubmission_DatabaseStorage\CourseSubmission_DatabaseStorage\Contexts\SqlDb.mdf;Integrated Security=True;Connect Timeout=30");
    }
    #endregion

    public DbSet<ClientEntity> Clients { get; set; }
    public DbSet<CaseEntity> Cases { get; set; }
    public DbSet<CaseCommentEntity> CaseComments { get; set; }
    public DbSet<ServiceWorkerEntity> ServiceWorkers { get; set; }
    public DbSet<CompanyEntity> Companies { get; set; }
    public DbSet<AdressEntity> Adresses { get; set; }

}
