using CourseSubmission_DatabaseStorage.Contexts;
using CourseSubmission_DatabaseStorage.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourseSubmission_DatabaseStorage.Services;

internal class CommentService : GenericService<CommentEntity>
{
    private readonly DataContext _context = new();

    public async Task CreateInitializedCommentAsync()
    {
        if (!await _context.Comments.AnyAsync())
        {
            await _context.AddAsync(new CommentEntity { Id = new Guid("e8622fa6-a076-4171-80f4-e5b05bfb6470"), TextComment = "Nu är det skottat utanför er entré och ärendet avslutat.", Created = DateTime.Parse("2023-03-14 15:00:23"), CaseId = Guid.Parse("f98b98fb-0470-4096-90cd-0b05d8be9fc9"), EmployeeId = 2 });
            await _context.SaveChangesAsync();

            await _context.AddAsync(new CommentEntity { Id = new Guid("fa324713-a369-42c0-8a93-896a1f3fad2a"), TextComment = "Problemet är åtgärdat och ärendet avslutat.", Created = DateTime.Parse("2023-03-13 10:08:54"), CaseId = Guid.Parse("6bad8146-7c0e-41d1-b646-377cd98d6c0e"), EmployeeId = 3 });
            await _context.SaveChangesAsync();

            await _context.AddAsync(new CommentEntity { Id = new Guid("192e0b5e-ed3a-4693-a535-f7496616af0b"), TextComment = "Ventilationsproblemet åtgärdat och ärendet avslutat.", Created = DateTime.Parse("2023-03-13 11:15:31"), CaseId = Guid.Parse("c5d7c1c5-be5c-441f-b756-3635616407af"), EmployeeId = 2 });
            await _context.SaveChangesAsync();
        }

    }
}
