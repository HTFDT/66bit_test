using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace web.Data;

public class Repository<T> : IRepository<T> where T: class, IEntityWithTypedId<Guid>
{
    protected DbContext Context { get; }
    protected DbSet<T> DbSet { get; }

    public Repository(ApplicationDbContext dbContext)
    {
        Context = dbContext;
        DbSet = Context.Set<T>();
    }

    public ValueTask<T?> FindById(Guid id, bool tracking = false)
    {
        return tracking ? DbSet.FindAsync(id) : new ValueTask<T?>(DbSet.AsNoTracking().FirstOrDefaultAsync(e => e.ID == id));
    }

    public Task<List<T>> GetAll()
    {
        return DbSet.AsNoTracking().ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await DbSet.AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await DbSet.AddRangeAsync(entities);
    }

    public IQueryable<T> Query()
    {
        return DbSet;
    }

    public void Remove(T entity)
    {
        DbSet.Remove(entity);
    }

    public Task SaveChangesAsync()
    {
        return Context.SaveChangesAsync();
    }
}