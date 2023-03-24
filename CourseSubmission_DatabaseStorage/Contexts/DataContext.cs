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
        optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\emmag\source\repos\Datalagring\CourseSubmission_DatabaseStorage\CourseSubmission_DatabaseStorage\Contexts\sql_db.mdf;Integrated Security=True;Connect Timeout=30");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClientEntity>()
                .HasIndex(x => x.Email)
                .IsUnique();
    }
    #endregion

    public DbSet<StatusTypeEntity> StatusTypes { get; set; }
    public DbSet<AdressEntity> Adresses { get; set; }
    public DbSet<RoleEntity> Roles { get; set; }
    public DbSet<EmployeeEntity> Employees { get; set; }
    public DbSet<ClientEntity> Clients { get; set; }
    public DbSet<CaseEntity> Cases { get; set; }
    public DbSet<CommentEntity> Comments { get; set; }

}
