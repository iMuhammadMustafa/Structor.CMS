using PostsService.Infrastructure.Entities;
using System.Linq.Expressions;

namespace PostsService.Infrastructure.Repositories;

public interface IRepository<TEntity>
                        where TEntity : IEntity
{
    IQueryable<TEntity> GetAll();
    IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] included);


    Task<TEntity?> FindById(int id);
    Task<TEntity?> FindByGuid(Guid guid);
    Task<TEntity?> FindFirstWhere(Expression<Func<TEntity, bool>> expression);
    Task<IEnumerable<TEntity>> FindAllWhere(Expression<Func<TEntity, bool>> expression);


    Task<int> Count();
    Task<int> CountWhere(Expression<Func<TEntity, bool>> expression);


    Task<TEntity> Add(TEntity entity, bool saveChanges = false);
    Task<TEntity> Update(TEntity entity, bool saveChanges = false);
    Task<bool> Delete(TEntity entity, bool saveChanges = false);

    Task<bool> SaveChanges();

}