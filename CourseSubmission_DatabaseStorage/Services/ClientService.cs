using CourseSubmission_DatabaseStorage.Contexts;
using CourseSubmission_DatabaseStorage.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CourseSubmission_DatabaseStorage.Services;

internal class ClientService : GenericService<ClientEntity>
{
    private readonly DataContext _context = new ();
    private readonly AdressService _adressService = new();

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
        var _adress = new AdressEntity();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\nFill in the following..");
        Console.Write("\nFirstname: ");
        Console.ForegroundColor = ConsoleColor.Gray;
        var _findFirstName = await GetAsync(x => x.FirstName == entity.FirstName);
        entity.FirstName = Console.ReadLine()!;
        if (string.IsNullOrEmpty(entity.FirstName))
        {
            entity.FirstName = _findFirstName.FirstName;
        }
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("Lastname: ");
        Console.ForegroundColor = ConsoleColor.Gray;
        var _findLastName = await GetAsync(x => x.LastName == entity.LastName);
        entity.LastName = Console.ReadLine()!;
        if (string.IsNullOrEmpty(entity.LastName))
        {
            entity.LastName = _findLastName.LastName;
        }
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("Phone number (ex. +4670-1234567): ");
        Console.ForegroundColor = ConsoleColor.Gray;
        var _findPhoneNumber = await GetAsync(x => x.PhoneNumber == entity.PhoneNumber);
        entity.PhoneNumber = Console.ReadLine()!;
        if (string.IsNullOrEmpty(entity.PhoneNumber))
        {
            entity.PhoneNumber = _findPhoneNumber.PhoneNumber;
        }
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("Streetname: ");
        Console.ForegroundColor = ConsoleColor.Gray;
        _adress.StreetName = Console.ReadLine()!;
        if (string.IsNullOrEmpty(_adress.StreetName))
            _adress.StreetName = entity.Adress.StreetName;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("Postalcode (ex. 12345): ");
        Console.ForegroundColor = ConsoleColor.Gray;
        _adress.PostalCode = Console.ReadLine()!;
        if (string.IsNullOrEmpty(_adress.PostalCode))
            _adress.PostalCode = entity.Adress.PostalCode;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("City: ");
        Console.ForegroundColor = ConsoleColor.Gray;
        _adress.City = Console.ReadLine()!;
        if (string.IsNullOrEmpty(_adress.City))
            _adress.City = entity.Adress.City;
        Console.ForegroundColor = ConsoleColor.Yellow;

        var _newAdress = await _adressService.SaveAsync(_adress, x => x.StreetName == _adress.StreetName && x.PostalCode == _adress.PostalCode && x.City == _adress.City);
        if (_newAdress != null)
        {
            entity.AdressId = _newAdress.Id;
        }

        _context.Update(entity);     
        await _context.SaveChangesAsync();
        return entity;
    }
}
