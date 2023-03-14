using CourseSubmission_DatabaseStorage.Contexts;
using CourseSubmission_DatabaseStorage.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CourseSubmission_DatabaseStorage.Services;

internal class ClientService : GenericService<ClientEntity>
{
    private readonly DataContext _context = new DataContext();

    public override async Task<IEnumerable<ClientEntity>> GetAllAsync()
    {
        return await _context.Clients.Include(x => x.Adress).ToListAsync();
    }

    public override async Task<ClientEntity> GetAsync(Expression<Func<ClientEntity, bool>> predicate)
    {
        var item = await _context.Clients.Include(x => x.Adress).Include(x => x.Cases).FirstOrDefaultAsync(predicate);

        if (item != null)
            return item;

        return null!;
    }


    /*
    private readonly DataContext _context = new DataContext();
    private readonly CaseService _caseService = new CaseService();

    public async Task<IEnumerable<ClientEntity>> GetAllAsync()
    {
        return await _context.Clients.Include(x => x.Cases).ToListAsync();
    }

    public async Task<ClientEntity> GetAsync(int id)
    {
        var clientEntity = await _context.Clients.Include(x => x.Cases).FirstOrDefaultAsync(x => x.Guid == id);
        if (clientEntity != null)
            return clientEntity;

        return null!;
    }

    public async Task DeleteAsync(int id)
    {
        var clientEntity = await _context.Clients.FirstOrDefaultAsync(x => x.Id == id);
        if (clientEntity != null)
        {
            _context.Remove(clientEntity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<ClientEntity> CreateAsync(CaseRegistrationForm form)
    {
        if (await _context.Clients.AnyAsync(x => x.Id == form.ClientId))
            return null!;

        var clientEntity = new ClientEntity()
        {
            Id = form.ClientId,
            FirstName = form.FirstName,
            LastName = form.LastName,
            Email = form.ClientEmail,
            PhoneNumber = form.ClientPhoneNumber,
            // Cases = (await _caseService.GetOrCreateIfNotExistsAsync(form.Cases)).Id
        };

        var caseEntity = new CaseEntity()
        {
            Description = form.CaseDescription,
            RegistrationDate = form.RegistrationDate,
            CaseStatus = form.CaseStatus,
        };


        _context.Add(clientEntity);
        await _context.SaveChangesAsync();

        return clientEntity;
    }
    */
}
