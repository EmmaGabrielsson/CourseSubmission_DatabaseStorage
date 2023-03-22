using CourseSubmission_DatabaseStorage.Contexts;
using CourseSubmission_DatabaseStorage.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Linq.Expressions;

namespace CourseSubmission_DatabaseStorage.Services;

internal class ClientService : GenericService<ClientEntity>
{
    private readonly DataContext _context = new ();

    public async Task CreateInitializedClientAsync()
    {
        if (!await _context.Clients.AnyAsync())
        {
            await _context.AddAsync(new ClientEntity { Id = new Guid("5de5b0ca-c9a6-4ac6-a5a3-c0bdeb9adb87"), FirstName = "Stina", LastName = "Marberg", Email = "stina@example.com", PhoneNumber = "+4673-2545123", AdressId = 3 });
            await _context.SaveChangesAsync();

            await _context.AddAsync(new ClientEntity { Id = new Guid("5f72fd36-b497-4481-b4dd-f6a98f12fb7a"), FirstName = "Emma", LastName = "Gabrielsson", Email = "emma@example.com", PhoneNumber = "+4673-8502456", AdressId = 2 });
            await _context.SaveChangesAsync();

            await _context.AddAsync(new ClientEntity { Id = new Guid("e39e810a-a16d-40d9-9fe3-44e2fd9bbe98"), FirstName = "Ingemar", LastName = "Eriksson", Email = "ingemar@example.com", PhoneNumber = "+4673-5696811", AdressId = 1 });
            await _context.SaveChangesAsync();

            await _context.AddAsync(new ClientEntity { Id = new Guid("5be79af7-42d0-43eb-a40f-c6c0896389da"), FirstName = "Siv", LastName = "Nordholm", Email = "siv@example.com", PhoneNumber = "+4672-2545123", AdressId = 4 });
            await _context.SaveChangesAsync();

            await _context.AddAsync(new ClientEntity { Id = new Guid("28ad7fbe-9365-4e69-8af3-7b07b879d090"), FirstName = "Sören", LastName = "Larsson", Email = "sören@example.com", PhoneNumber = "+4670-5896332", AdressId = 5 });
            await _context.SaveChangesAsync();

            await _context.AddAsync(new ClientEntity { Id = new Guid("a45633ad-a9c7-4edc-9561-338bfcfe4c8d"), FirstName = "Hans", LastName = "Mattin-Lassei", Email = "hans@example.com", PhoneNumber = "+4670-2583699", AdressId = 6 });
            await _context.SaveChangesAsync();
        }
    }

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

    public override async Task<ClientEntity> UpdateAsync(ClientEntity entity)
    {
        _context.Update(entity);     
        await _context.SaveChangesAsync();
        return entity;
    }
}
