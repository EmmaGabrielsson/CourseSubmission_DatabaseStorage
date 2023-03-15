using CourseSubmission_DatabaseStorage.Contexts;
using CourseSubmission_DatabaseStorage.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CourseSubmission_DatabaseStorage.Services;

internal class EmployeeService : GenericService<EmployeeEntity>
{
    private readonly DataContext _context = new();
    public override async Task<EmployeeEntity> GetAsync(Expression<Func<EmployeeEntity, bool>> predicate)
    {
        var item = await _context.Employees.Include(x => x.Role).FirstOrDefaultAsync(predicate);

        if (item != null)
            return item;

        return null!;
    }
}
