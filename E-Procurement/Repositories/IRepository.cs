﻿using System.Linq.Expressions;

namespace E_Procurement.Repositories;

public interface IRepository<TEntity>
{
    Task<TEntity> Save(TEntity entity); // Save
    TEntity Attach(TEntity entity);
    Task<IEnumerable<TEntity>> SaveAll(IEnumerable<TEntity> entities);
    // Find
    Task<TEntity?> FindById(Guid id); // Only find by id
    Task<TEntity?> Find(Expression<Func<TEntity, bool>> criteria);
    Task<TEntity?> Find(Expression<Func<TEntity, bool>> criteria, string[] includes);
    // Find All
    Task<IEnumerable<TEntity>> FindAll();
    Task<IEnumerable<TEntity>> FindAll(string[] includes);
    Task<IEnumerable<TEntity>> FindAll(Expression<Func<TEntity, bool>> criteria);
    Task<IEnumerable<TEntity>> FindAll(Expression<Func<TEntity, bool>> criteria, string[] includes);
    Task<IEnumerable<TEntity>> FindAll(Expression<Func<TEntity, bool>> criteria, int page, int size);
    Task<IEnumerable<TEntity>> FindAll(Expression<Func<TEntity, bool>> criteria, int page, int size, string[] includes);
    Task<IEnumerable<TEntity>> FindAll(Expression<Func<TEntity, bool>> criteria, int? page, int? size, string[]? includes, Expression<Func<TEntity, object>>? orderBy, string direction);
    
    TEntity Update(TEntity entity);
    void Delete(TEntity entity);
    void DeleteAll(IEnumerable<TEntity> entities);
    Task<int> Count();
    Task<int> Count(Expression<Func<TEntity, bool>> criteria);
}