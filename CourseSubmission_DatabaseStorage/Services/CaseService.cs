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
        return await _context.Cases
            .Include(x => x.Client)
            .Include(x => x.StatusType)
            .ToListAsync();
    }

    public virtual async Task<IEnumerable<CaseEntity>> GetAllActiveAsync()
    {
        return await _context.Cases
        .Include(x => x.Client)
        .Include(x => x.StatusType)
        .Where(x => x.StatusType.StatusName.ToLower() != "completed" )
        .OrderByDescending(x => x.RegistrationDate)
        .ToListAsync();

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

    public async Task<CaseEntity> UpdateCaseStatusAsync(Guid caseId, int statusId)
    {
        var _entity = await _context.Cases.FindAsync(caseId);

        if(_entity != null)
        {
            _entity.UpdatedDate = DateTime.Now;
            _entity.StatusTypeId = statusId;
            _context.Update(_entity);
            await _context.SaveChangesAsync();
            return _entity;
        }
        return null!;
    }
}
