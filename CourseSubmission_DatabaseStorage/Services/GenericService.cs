using CourseSubmission_DatabaseStorage.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CourseSubmission_DatabaseStorage.Services;

internal abstract class GenericService<TEntity> where TEntity : class
{
    private readonly DataContext _context = new();

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _context.Set<TEntity>().ToListAsync();
    }

    public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var _item = await _context.Set<TEntity>().FirstOrDefaultAsync(predicate, CancellationToken.None);
        if (_item != null)
            return _item;

        return null!;
    }

    public virtual async Task<TEntity> GetOrCreateAsync(TEntity entity,Expression<Func<TEntity, bool>> predicate)
    {
        var _item = await _context.Set<TEntity>().FirstOrDefaultAsync(predicate, CancellationToken.None);
        if (_item != null)
            return _item;
        else
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }

    public virtual async Task<TEntity> SaveAsync(TEntity entity)
    {
        _context.Add(entity);
        await _context.SaveChangesAsync(); 
        return entity;
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        _context.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task DeleteAsync(TEntity entity)
    {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
    }

}