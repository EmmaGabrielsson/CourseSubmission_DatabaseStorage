using CourseSubmission_DatabaseStorage.Contexts;
using CourseSubmission_DatabaseStorage.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CourseSubmission_DatabaseStorage.Services;

internal class EmployeeService : GenericService<EmployeeEntity>
{
    private readonly DataContext _context = new();
    public async Task CreateInitializedEmployeeAsync()
    {
        if (!await _context.Employees.AnyAsync())
        {
            await _context.AddAsync(new EmployeeEntity { FirstName = "Lisa", LastName = "Larsson", RoleId = 2 });
            await _context.SaveChangesAsync();

            await _context.AddAsync(new EmployeeEntity { FirstName = "Anna", LastName = "Persson", RoleId = 3 });
            await _context.SaveChangesAsync();

            await _context.AddAsync(new EmployeeEntity { FirstName = "Bo", LastName = "Svensson", RoleId = 1 });
            await _context.SaveChangesAsync();

            await _context.AddAsync(new EmployeeEntity { FirstName = "Theo", LastName = "Eriksson", RoleId = 2 });
            await _context.SaveChangesAsync();
        }

    }
    public override async Task<IEnumerable<EmployeeEntity>> GetAllAsync()
    {
        return await _context.Employees
                    .Include(x => x.Role)
                    .OrderByDescending(x => x.RoleId)
                    .ToListAsync();
    }

    public override async Task<EmployeeEntity> GetAsync(Expression<Func<EmployeeEntity, bool>> predicate)
    {
        var item = await _context.Employees.Include(x => x.Role).FirstOrDefaultAsync(predicate);

        if (item != null)
            return item;

        return null!;
    }

}
