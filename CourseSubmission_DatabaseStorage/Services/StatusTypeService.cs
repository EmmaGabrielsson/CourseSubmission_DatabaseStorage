using CourseSubmission_DatabaseStorage.Contexts;
using CourseSubmission_DatabaseStorage.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourseSubmission_DatabaseStorage.Services;

internal class StatusTypeService : GenericService<StatusTypeEntity>
{
    private readonly DataContext _context = new();
    public async Task CreateInitializedStatusAsync()
    {
        if (!await _context.StatusTypes.AnyAsync())
        {
            string[] _statuses = new string[] {"Not Started", "Ongoing", "Completed" };

            foreach (var status in _statuses)
            {
                await _context.AddAsync(new StatusTypeEntity { StatusName = status });
                await _context.SaveChangesAsync();
            }
        }
    }
}
