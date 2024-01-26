using Microsoft.EntityFrameworkCore.Storage;

namespace web.Data;

public interface IRepository<T> : IRepositoryWithTypedId<T, Guid> where T: IEntityWithTypedId<Guid>
{
    
}