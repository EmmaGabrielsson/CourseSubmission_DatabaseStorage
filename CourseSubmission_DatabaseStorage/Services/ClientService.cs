using CourseSubmission_DatabaseStorage.Contexts;
using CourseSubmission_DatabaseStorage.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CourseSubmission_DatabaseStorage.Services;

internal class ClientService : GenericService<ClientEntity>
{
    private readonly DataContext _context = new DataContext();
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
        var _updatedEntity = new ClientEntity();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\nFill in the following..");
        Console.Write("\nFirstname: ");
        Console.ForegroundColor = ConsoleColor.Gray;
        _updatedEntity.FirstName = Console.ReadLine() ?? "";
        if (string.IsNullOrEmpty(_updatedEntity.FirstName))
            _updatedEntity.FirstName = entity.FirstName;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("Lastname: ");
        Console.ForegroundColor = ConsoleColor.Gray;
        _updatedEntity.LastName = Console.ReadLine() ?? "";
        if (string.IsNullOrEmpty(_updatedEntity.LastName))
            _updatedEntity.LastName = entity.LastName;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("Telephone number: ");
        Console.ForegroundColor = ConsoleColor.Gray;
        _updatedEntity.PhoneNumber = Console.ReadLine() ?? "";
        if (string.IsNullOrEmpty(_updatedEntity.PhoneNumber))
            _updatedEntity.PhoneNumber = entity.PhoneNumber;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("Streetname: ");
        Console.ForegroundColor = ConsoleColor.Gray;
        _adress.StreetName = Console.ReadLine() ?? "";
        if (string.IsNullOrEmpty(_adress.StreetName))
            _adress.StreetName = entity.Adress.StreetName;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("Postalcode (5): ");
        Console.ForegroundColor = ConsoleColor.Gray;
        _adress.PostalCode = Console.ReadLine() ?? "";
        if (string.IsNullOrEmpty(_adress.PostalCode))
            _adress.PostalCode = entity.Adress.PostalCode;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("City: ");
        Console.ForegroundColor = ConsoleColor.Gray;
        _adress.City = Console.ReadLine() ?? "";
        if (string.IsNullOrEmpty(_adress.City))
            _adress.City = entity.Adress.City;
        Console.ForegroundColor = ConsoleColor.Yellow;

        var _newAdress = await _adressService.SaveAsync(_adress);
        if (_newAdress != null)
        {
            _updatedEntity.AdressId = _newAdress.Id;
            _updatedEntity.Adress = _newAdress;
        }
        else
        {
            _updatedEntity.Adress = entity.Adress;
            _updatedEntity.AdressId = entity.AdressId;
        }

        _updatedEntity.Id = entity.Id;
        _updatedEntity.Email = entity.Email;

        _context.Update(_updatedEntity);     
        await _context.SaveChangesAsync();
        return _updatedEntity;
    }
}
