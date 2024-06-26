﻿using Microsoft.EntityFrameworkCore;
using PostsService.Infrastructure.Entities;
using System.Linq.Expressions;

namespace PostsService.Infrastructure.Repositories;

//TODO: Add AsNoTracking Tracking functionality (bool tracked = false maybe) 
public abstract class Repository<TEntity, TContext> : IRepository<TEntity>
                                     where TEntity : IEntity
                                     where TContext : DbContext
{
    private readonly TContext _context;

    public Repository(TContext context)
    {
        _context = context;
        //_context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    private DbSet<TEntity>? _dbSet;

    protected DbSet<TEntity> DbSet
    {
        get
        {
            if (_dbSet == null) _dbSet = _context.Set<TEntity>();
            return _dbSet;
        }
    }

    public virtual IQueryable<TEntity> GetAll() => DbSet.AsQueryable();

    public virtual async Task<TEntity?> FindById(int id) => await DbSet.FirstOrDefaultAsync(Entity => Entity.Id == id) ?? null;

    public virtual async Task<TEntity?> FindByGuid(Guid guid) => await DbSet.FirstOrDefaultAsync(Entity => Entity.Guid == guid) ?? null;
    public virtual async Task<TEntity?> FindFirstWhere(Expression<Func<TEntity, bool>> predicate) => await DbSet.FirstOrDefaultAsync(predicate);
    public virtual async Task<IEnumerable<TEntity>> FindAllWhere(Expression<Func<TEntity, bool>> predicate) => await DbSet.Where(predicate).ToListAsync();
    public virtual IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] included)
    {
        var queryable = DbSet.AsQueryable();

        foreach (var include in included)
        {
            queryable = queryable.Include(include);
        }
        return queryable;
    }

    public virtual async Task<int> Count() => await DbSet.CountAsync();
    public virtual async Task<int> CountWhere(Expression<Func<TEntity, bool>> expression) => await DbSet.Where(expression).CountAsync();

    public virtual async Task<TEntity> Add(TEntity entity, bool saveChanges = false)
    {
        //_context.ChangeTracker.DetectChanges();
        var dbEntity = await DbSet.AddAsync(entity);
        if (saveChanges)
        {
            await SaveChanges();
        }
        return dbEntity.Entity;
    }
    public virtual async Task<TEntity> Update(TEntity entity, bool saveChanges = false)
    {
        var entry = DbSet.Attach(entity);
        entry.State = EntityState.Modified;

        if (saveChanges)
        {
            await SaveChanges();
        }

        return entry.Entity;
    }
    public virtual async Task<bool> Delete(TEntity entity, bool saveChanges = false)
    {
        //DbSet.Attach(entity);
        _context.ChangeTracker.DetectChanges();
        _context.Remove(entity);
        if (saveChanges)
        {
            return await SaveChanges();
        }

        return true;
    }

    public async Task<bool> SaveChanges()
    {
        await _context.SaveChangesAsync();
        return true;
    }
}



//public IQueryable<TEntity> FindAllOrdered<TProperty>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TProperty>> orderBy, bool orderDesc = false)
//{
//    var queryable = DbSet.Where(expression);

//    queryable = orderDesc ? queryable.OrderByDescending(orderBy)
//                          : queryable.OrderBy(orderBy);

//    return queryable;
//}

//public IQueryable<TEntity> FindAllPaged<TProperty>(Expression<Func<TEntity, bool>> expression, int pageNumber, int pageSize)
//{
//    var queryable = DbSet.Where(expression);
//    queryable = queryable.Skip((pageNumber - 1) * pageSize)
//                          .Take(pageSize);

//    return queryable;
//}


//public virtual async Task<TEntity> FindWhere(Expression<Func<TEntity, bool>> expression) => await DbSet.Where(expression).FirstOrDefaultAsync() ?? null!;


//public virtual async Task<IList<TEntity>> FindAllWhere(Expression<Func<TEntity, bool>> expression) => await DbSet.Where(expression).ToListAsync();


//public virtual async Task<IList<TEntity>> FindAllPaginatedWhere<TProperty>(Expression<Func<TEntity, bool>> expression, int pageNumber = 0, int pageSize = 10, Expression<Func<TEntity, TProperty>>? orderBy = null, bool? orderDesc = false)
//{
//    var queryable = DbSet.AsQueryable();
//    queryable = queryable.Where(expression);

//    queryable = queryable.Skip((pageNumber - 1) * pageSize).Take(pageSize);


//    if (orderBy != null) queryable = queryable.OrderBy(orderBy);



//    return await queryable.ToListAsync();

//}

//public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, int pageNumber, int pageSize)
//{
//    return queryable
//        .Skip((pageNumber - 1) * pageSize)
//        .Take(pageSize);
//}
//public virtual Task<IList<TEntity>> FindPaginatedWhere<TProperty>(Expression<Func<TEntity, bool>> expression, int pageNumber, int pageSize)
//{
//    throw new NotImplementedException();
//}

//public Task<IQueryable<TEntity>> Order<TProperty>(Expression<Func<TEntity, TProperty>> orderBy, bool? orderDesc)
//{
//    throw new NotImplementedException();
//}

//public IQueryable<TEntity> OrderX<TProperty>(Func<TEntity, TProperty> orderBy, bool? orderDesc)
//{
//    return DbSet.Where(x => x.Id == 5).OrderBy(t => orderBy(t));
//}

//public static IQueryable<TEntity> Paginate(this IQueryable<TEntity> entities, int pageNumber, int pageSize)
//{
//    return entities.Skip((pageNumber - 1) * pageSize)
//                   .Take(pageSize);
//}