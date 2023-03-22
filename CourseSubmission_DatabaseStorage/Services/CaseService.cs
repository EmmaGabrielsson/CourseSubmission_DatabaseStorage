using CourseSubmission_DatabaseStorage.Contexts;
using CourseSubmission_DatabaseStorage.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CourseSubmission_DatabaseStorage.Services;

internal class CaseService : GenericService<CaseEntity>
{
    private readonly DataContext _context = new ();

    public async Task CreateInitializedCaseAsync()
    {
        if (!await _context.Cases.AnyAsync())
        {
            await _context.AddAsync(new CaseEntity { Id = new Guid("f98b98fb-0470-4096-90cd-0b05d8be9fc9"), Title = "Snöras", Description = "Det har rasat ner jättemycket snö från taket rakt ovanför entrén, så just nu är det svårt att ta sig ut där.", RegistrationDate = DateTime.Parse("2023-03-14 14:08:54"), UpdatedDate = DateTime.Parse("2023-03-14 16:00:23"), StatusTypeId = 3, ClientId = Guid.Parse("28ad7fbe-9365-4e69-8af3-7b07b879d090") });
            await _context.SaveChangesAsync();

            await _context.AddAsync(new CaseEntity { Id = new Guid("6bad8146-7c0e-41d1-b646-377cd98d6c0e"), Title = "Problem med fibernätet", Description = "Kopplingsdosan till fibernätet i lägenheten blinkar rött och fungerar inte riktigt. Provat starta om den, men den hittar ingen koppling.", RegistrationDate = DateTime.Parse("2023-03-10 15:28:14"), UpdatedDate = DateTime.Parse("2023-03-10 15:28:14"), StatusTypeId = 3, ClientId = Guid.Parse("5be79af7-42d0-43eb-a40f-c6c0896389da") });
            await _context.SaveChangesAsync();

            await _context.AddAsync(new CaseEntity { Id = new Guid("c5d7c1c5-be5c-441f-b756-3635616407af"), Title = "Ventilationsfel", Description = "Det låter konstigt från ventilationen och fläkten i köket drar inte ut matoset, måste kontrolleras snarast.", RegistrationDate = DateTime.Parse("2023-03-11 10:47:02"), UpdatedDate = DateTime.Parse("2023-03-13 11:15:31"), StatusTypeId = 3, ClientId = Guid.Parse("5f72fd36-b497-4481-b4dd-f6a98f12fb7a") });
            await _context.SaveChangesAsync();
        }

    }
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
