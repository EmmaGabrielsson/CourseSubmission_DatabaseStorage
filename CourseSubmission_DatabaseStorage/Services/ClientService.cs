﻿using CourseSubmission_DatabaseStorage.Contexts;
using CourseSubmission_DatabaseStorage.Models.Entities;
using Microsoft.EntityFrameworkCore;
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
        _context.Clients.Update(entity);     
        await _context.SaveChangesAsync();
        return entity;
    }

    public override async Task<bool> DeleteAsync(ClientEntity entity)
    {
        var _findClientEntity = await _context.Clients.FindAsync(entity.Id);
        if (_findClientEntity == null)
        {
            // handle client not found error
            return false;
        }

        IEnumerable<CaseEntity> _activeCasesOnClient = await _context.Cases
            .Where(x => x.ClientId == _findClientEntity!.Id && 
            (x.StatusType.StatusName.ToLower() == "not started"
            || x.StatusType.StatusName.ToLower() == "ongoing"))
            .ToListAsync();

        if (!_activeCasesOnClient.Any())
        {
            _context.Clients.Remove(_findClientEntity!);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}
