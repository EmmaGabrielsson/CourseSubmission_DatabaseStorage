using CourseSubmission_DatabaseStorage.Contexts;
using CourseSubmission_DatabaseStorage.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CourseSubmission_DatabaseStorage.Services;

internal class CaseService : GenericService<CaseEntity>
{
    private readonly DataContext _context = new ();

    public override async Task<IEnumerable<CaseEntity>> GetAllAsync()
    {
        return await _context.Cases.Include(x => x.Client).Include(x => x.StatusType).ToListAsync();
    }

    public override async Task<CaseEntity> GetAsync(Expression<Func<CaseEntity, bool>> predicate)
    {
        var item = await _context.Cases
            .Include(x => x.Client).ThenInclude(x => x.Adress)
            .Include(x => x.StatusType)
            .Include(x => x.Comments).ThenInclude(x => x.Employee).ThenInclude(x => x.Role)
            .FirstOrDefaultAsync(predicate);

        if (item != null)
            return item;

        return null!;
    }
}
