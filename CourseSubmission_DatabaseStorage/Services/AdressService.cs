using CourseSubmission_DatabaseStorage.Contexts;
using CourseSubmission_DatabaseStorage.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourseSubmission_DatabaseStorage.Services;

internal class AdressService : GenericService<AdressEntity>
{
    private readonly DataContext _context = new();
    public async Task CreateInitializedAdressAsync()
    {
        if(!await _context.Adresses.AnyAsync()) 
        { 
            await _context.AddAsync(new AdressEntity { StreetName = "Sveavägen 3", PostalCode = "34512", City = "Göteborg" });
            await _context.SaveChangesAsync();

            await _context.AddAsync(new AdressEntity { StreetName = "Gamlavägen 12", PostalCode = "27561", City = "Göteborg" });
            await _context.SaveChangesAsync();

            await _context.AddAsync(new AdressEntity { StreetName = "Floravägen 60", PostalCode = "12345", City = "Göteborg" });
            await _context.SaveChangesAsync();

            await _context.AddAsync(new AdressEntity { StreetName = "Ängsvägen 22", PostalCode = "54321", City = "Göteborg" });
            await _context.SaveChangesAsync();

            await _context.AddAsync(new AdressEntity { StreetName = "Villagatan 56", PostalCode = "65425", City = "Göteborg" });
            await _context.SaveChangesAsync();

            await _context.AddAsync(new AdressEntity { StreetName = "Nordkapsvägen 14", PostalCode = "25469", City = "Göteborg" });
            await _context.SaveChangesAsync();
        }
    }
}
