using System.Linq.Expressions;
using IBBS.Infrastructure.Persistence.Adapters.Contracts;
using IBBS.Infrastructure.Persistence.Adapters.Helpers;
using Microsoft.EntityFrameworkCore;

namespace IBBS.Infrastructure.Persistence.Adapters.Repositories;

/// <summary>
/// The implementation of the generic repository.
/// </summary>
/// <typeparam name="TEntity">The generic entity.</typeparam>
/// <param name="context">The database context.</param>
public sealed class GenericRepository<TEntity>(SqlDbContext context) : IRepository<TEntity> where TEntity : class
{
    /// <inheritdoc />
    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await context.Set<TEntity>()
            .AddAsync(entity, cancellationToken)
            .ConfigureAwait(false);
        return entity;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await context.Set<TEntity>()
            .AddRangeAsync(entities, cancellationToken)
            .ConfigureAwait(false);
        return entities;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<TEntity>> FindAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default
    ) =>
        await context.Set<TEntity>()
            .AsNoTracking()
            .Where(predicate)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

    /// <inheritdoc />
    public async Task<TEntity?> FirstOrDefaultAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default
    ) =>
        await context.Set<TEntity>()
            .AsNoTracking()
            .FirstOrDefaultAsync(predicate, cancellationToken)
            .ConfigureAwait(false);

    /// <inheritdoc />
    public async Task<List<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        string? includeProperties = null,
        int pageSize = 0,
        int pageNumber = 1,
        bool isActiveOnly = true,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = context.Set<TEntity>();
        query = query.WhereIsActive(isActiveOnly);

        if (filter is not null)
            query = query.Where(filter);

        if (pageSize > 0)
        {
            if (pageSize > 100) pageSize = 100;
            query = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
        }

        if (includeProperties is not null)
            foreach (var includeProperty in includeProperties.Split([','], StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

        return await query.AsNoTracking()
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<(List<TEntity>, int, bool)> GetAllPagedAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Expression<Func<TEntity, object>>? orderByProperty = null,
        bool ascending = true,
        int pageSize = 1000,
        int pageNumber = 1,
        params Expression<Func<TEntity, object>>[] includeProperties)
    {
        bool hasNextPage = false;
        IQueryable<TEntity> query = context.Set<TEntity>();
        if (filter is not null)
            query = query.Where(filter);

        if (orderByProperty is not null)
            query = ascending ? query.OrderBy(orderByProperty) : query.OrderByDescending(orderByProperty);

        int totalDbRecords = await query.CountAsync();
        if (pageSize > 0)
            query = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize + 1);

        if (includeProperties is not null)
            foreach (var includeProperty in includeProperties)
                query = query.Include(includeProperty);

        var result = await query.AsNoTracking().ToListAsync().ConfigureAwait(false);
        if (result.Count > pageSize)
        {
            hasNextPage = true;
            result.RemoveAt(result.Count - 1);
        }

        return (result, totalDbRecords, hasNextPage);
    }

    /// <inheritdoc />
    public async Task<TEntity> GetAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        bool tracked = true,
        string? includeProperties = null,
        bool isActiveOnly = true,
        CancellationToken cancellationToken = default
    )
    {
        IQueryable<TEntity> query = context.Set<TEntity>();
        query = query.WhereIsActive(isActiveOnly);
        if (!tracked)
            query = query.AsNoTracking();

        if (filter is not null)
            query = query.Where(filter);

        if (includeProperties is not null)
            foreach (var includedProperty in includeProperties.Split([','], StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includedProperty);

        return await query.AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false) ?? default!;
    }

    /// <inheritdoc />
    public void Remove(TEntity entity)
    {
        context.Set<TEntity>().Remove(entity);
    }

    /// <inheritdoc />
    public void RemoveRange(IEnumerable<TEntity> entities)
    {
        context.Set<TEntity>().RemoveRange(entities);
    }

    /// <inheritdoc />
    public async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default
    ) =>
        await context.SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);

    /// <inheritdoc />
    public TEntity Update(TEntity entity)
    {
        context.Set<TEntity>().Attach(entity);
        context.Entry(entity).State = EntityState.Modified;
        return entity;
    }
}