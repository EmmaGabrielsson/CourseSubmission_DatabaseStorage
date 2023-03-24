using CourseSubmission_DatabaseStorage.Contexts;
using CourseSubmission_DatabaseStorage.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourseSubmission_DatabaseStorage.Services;

internal class RoleService : GenericService<RoleEntity>
{
    private readonly DataContext _context = new();
    public async Task CreateInitializedRoleAsync()
    {
        if (!await _context.Roles.AnyAsync())
        {
            string[] _roles = new string[] { "IT Support Technician", "Property Caretaker", "Administrator" };

            foreach (var role in _roles)
            {
                await _context.Roles.AddAsync(new RoleEntity { RoleName = role });
                await _context.SaveChangesAsync();
            }
        }
    }

}
