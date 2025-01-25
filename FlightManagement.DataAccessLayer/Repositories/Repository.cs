using FlightManagement.DataAccessLayer.Entities;
using FlightManagement.DataAccessLayer.Repositories.Interfaces;
using FlightManagement.Framework.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagement.DataAccessLayer.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;
        private readonly CancellationToken _cancellationToken;

        public Repository(DbContext dbContext, CancellationToken cancellationToken)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
            _cancellationToken = cancellationToken;
        }

        public class FilterBuilder : IFilterBuilder<TEntity>
        {
            private readonly List<Expression<Func<TEntity, bool>>> filters;
            public FilterBuilder()
            {
                filters = new List<Expression<Func<TEntity, bool>>>();
            }

            public IFilterBuilder<TEntity> Add(Expression<Func<TEntity, bool>> filter)
            {
                filters.Add(filter);
                return this;
            }

            public IEnumerable<Expression<Func<TEntity, bool>>> Build()
            {
                return filters;
            }
        }

        public IFilterBuilder<TEntity> GetFilterBuilder => new FilterBuilder();


        public async Task<List<TEntity>> GetAsync<OUT>(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            int skip = 0,
            int take = -1,
            params string[] populate)
        {
            var filters = GetFilterBuilder;
            if (filter is not null)
                filters.Add(filter);

            var data = await GetAsync(select: s => s, filters.Build(), orderBy, skip, take, populate);
            return data;
        }


        public async Task<List<OUT>> GetAsync<OUT>(
            Expression<Func<TEntity, OUT>> select,
            IEnumerable<Expression<Func<TEntity, bool>>>? filters = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            int skip = 0,
            int take = -1,
            params string[] populate)
        {
            IQueryable<TEntity> query = _dbSet.AsNoTracking();

            if (filters != null)
                foreach (var f in filters)
                    query = query.Where(f);

            foreach (var includeProperty in populate)
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
                query = orderBy(query);

            query = query.Skip(skip).Take(take == -1 ? int.MaxValue : take);

            return await query.Select(select).ToListAsync(_cancellationToken);
        }

        public IQueryable<TEntity> GetAsyncIQueryable(IEnumerable<Expression<Func<TEntity, bool>>>? filters = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, int skip = 0, int take = -1, params string[] populate)
        {
            IQueryable<TEntity> query = _dbSet.AsNoTracking();

            if (filters != null)
                foreach (var f in filters)
                    query = query.Where(f);

            foreach (var includeProperty in populate)
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
                query = orderBy(query);

            query = query.Skip(skip).Take(take == -1 ? int.MaxValue : take);



            return query;
        }

        public async Task<List<OUT>> GetAsync<OUT>(
            Func<IQueryable, IQueryable<OUT>> select,
            IEnumerable<Expression<Func<TEntity, bool>>>? filters = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            int skip = 0,
            int take = -1,
            params string[] populate)
        {
            IQueryable<TEntity> query = _dbSet.AsNoTracking();

            if (filters != null)
                foreach (var f in filters)
                    query = query.Where(f);

            foreach (var includeProperty in populate)
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
                query = query.OrderBy(s => orderBy);

            query = query.Skip(skip).Take(take == -1 ? int.MaxValue : take);

            return await select(query).ToListAsync(_cancellationToken);
        }

        public async Task<TEntity> GetByIdAsync(Guid id, params string[] includes)
        {
            IQueryable<TEntity> query = _dbSet.AsQueryable();

            foreach (string includeProperty in includes)
            {
                query = query.Include(includeProperty);
            }

            return await query.FirstOrDefaultAsync(s => s.Id == id, _cancellationToken) ?? throw new NotFoundException(nameof(TEntity) + " Not Found");
        }
        public async Task<TEntity> GetByIdAsync(object id, params string[] includes)
        {
            TEntity? data = await _dbSet.FindAsync(id, _cancellationToken);
            return data ?? throw new NotFoundException(nameof(TEntity) + " Not Found");
        }

        public async Task<TEntity> GetByIdAsyncAsNoTracking(Guid id, params string[] includes)
        {
            IQueryable<TEntity> query = _dbSet.AsNoTracking();

            foreach (string includeProperty in includes)
            {
                query = query.Include(includeProperty);
            }

            return await query.FirstOrDefaultAsync(s => s.Id == id, _cancellationToken) ?? throw new NotFoundException(nameof(TEntity) + " Not Found");
        }
        public async Task<TEntity> GetByAsync(Expression<Func<TEntity, bool>> filter, params string[] includes)
        {
            IQueryable<TEntity> query = _dbSet;
            foreach (string includeProperty in includes)
            {
                query = query.Include(includeProperty);
            }

            TEntity? data = await query.FirstOrDefaultAsync(filter, _cancellationToken);
            return data ?? throw new NotFoundException(nameof(TEntity) + " Not Found");
        }
        public async Task<TEntity?> GetByAsyncNullable(Expression<Func<TEntity, bool>> filter, params string[] includes)
        {
            IQueryable<TEntity> query = _dbSet;
            foreach (string includeProperty in includes)
            {
                query = query.Include(includeProperty);
            }

            return await query.FirstOrDefaultAsync(filter, _cancellationToken);
        }

        public Task AddAsync(TEntity entity)
        {
            _dbSet.Add(entity);
            return Task.CompletedTask;
        }
        public Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
            return Task.CompletedTask;
        }

        public Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            _dbContext.UpdateRange(entities);
            return Task.CompletedTask;
        }
        public async Task UpdateAsync(TEntity entity)
        {
            _dbContext.Update(entity);
        }

        public Task DeleteAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
            return Task.CompletedTask;
        }
        public Task DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
            return Task.CompletedTask;
        }
        public async Task<bool> Exists(Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> query = _dbSet;
            return await query.AnyAsync(filter, _cancellationToken);
        }
        public async Task<int> CountAsync(IEnumerable<Expression<Func<TEntity, bool>>>? filters = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filters != null)
                foreach (var f in filters)
                    query = query.Where(f);

            return await query.CountAsync(_cancellationToken);
        }
        public async Task<decimal> SumAsync(Expression<Func<TEntity, decimal>>? selector, Expression<Func<TEntity, bool>>? filter = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
                query = query.Where(filter);

            return await query.SumAsync(selector, _cancellationToken);
        }
        public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? filter = null)
        {
            return filter != null ? await _dbSet.CountAsync(filter, _cancellationToken) : await _dbSet.CountAsync(_cancellationToken);
        }
        public async Task<int> CountDistinct<K>(Expression<Func<TEntity, K>> distinctBy, Expression<Func<TEntity, bool>>? filter = null)
        {
            return filter != null ? await _dbSet.DistinctBy(distinctBy).CountAsync(filter, _cancellationToken) : await _dbSet.DistinctBy(distinctBy).CountAsync(_cancellationToken);
        }
        public async Task<int> CountDistinct<TKey>(Expression<Func<TEntity, TKey>> keySelector, IEnumerable<Expression<Func<TEntity, bool>>>? filters = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filters is not null)
                foreach (var f in filters)
                    query = query.Where(f);


            return await query.Select(keySelector).Distinct().CountAsync(_cancellationToken);
        }
        public async Task<TKey> Max<TKey>(Expression<Func<TEntity, TKey>> keySelector, IEnumerable<Expression<Func<TEntity, bool>>>? filters = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filters is not null)
                foreach (var f in filters)
                    query = query.Where(f);

            return await query.MaxAsync(keySelector);
        }
        public async Task<TKey> Min<TKey>(Expression<Func<TEntity, TKey>> keySelector, IEnumerable<Expression<Func<TEntity, bool>>>? filters = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filters is not null)
                foreach (var f in filters)
                    query = query.Where(f);

            return await query.MinAsync(keySelector);
        }
        public async Task ReloadAsync(TEntity entity)
        {
            await _dbSet.Entry(entity).ReloadAsync();
        }
        public async Task<List<TEntity>> GetAsyncWithoutSelect(IEnumerable<Expression<Func<TEntity, bool>>>? filters = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, int skip = 0, int take = -1, params string[] populate)
        {
            IQueryable<TEntity> query = _dbSet.AsNoTracking();

            if (filters != null)
                foreach (var f in filters)
                    query = query.Where(f);

            foreach (var includeProperty in populate)
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
                query = orderBy(query);

            query = query.Skip(skip).Take(take == -1 ? int.MaxValue : take);

            return await query.ToListAsync(_cancellationToken);
        }

    }
}
