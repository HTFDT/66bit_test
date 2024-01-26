namespace web.Data;

public interface IRepositoryWithTypedId<T, TId> where T: IEntityWithTypedId<TId>
{
    ValueTask<T?> FindById(TId id, bool tracking = false);
    Task<List<T>> GetAll();
    Task AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);
    IQueryable<T> Query();
    void Remove(T entity);
    Task SaveChangesAsync();
}