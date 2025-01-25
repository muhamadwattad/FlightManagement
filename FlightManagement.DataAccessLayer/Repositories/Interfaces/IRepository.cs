using FlightManagement.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagement.DataAccessLayer.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        IFilterBuilder<TEntity> GetFilterBuilder { get; }



        Task<List<OUT>> GetAsync<OUT>(Expression<Func<TEntity, OUT>> select, IEnumerable<Expression<Func<TEntity, bool>>>? filters = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, int skip = 0, int take = -1, params string[] populate);
        IQueryable<TEntity> GetAsyncIQueryable(IEnumerable<Expression<Func<TEntity, bool>>>? filters = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, int skip = 0, int take = -1, params string[] populate);
        Task<List<TEntity>> GetAsyncWithoutSelect(IEnumerable<Expression<Func<TEntity, bool>>>? filters = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, int skip = 0, int take = -1, params string[] populate);



        Task<TEntity> GetByIdAsync(object id, params string[] includes);
        Task<TEntity> GetByIdAsync(Guid id, params string[] includes);
        Task<TEntity> GetByIdAsyncAsNoTracking(Guid id, params string[] includes);
        Task ReloadAsync(TEntity entity);

        Task<TEntity> GetByAsync(Expression<Func<TEntity, bool>> filter, params string[] includes);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        Task DeleteAsync(TEntity entityToDelete);
        Task DeleteRangeAsync(IEnumerable<TEntity> entitiesToDelete);


        Task UpdateRangeAsync(IEnumerable<TEntity> entities);
        Task UpdateAsync(TEntity entity);

        Task<decimal> SumAsync(Expression<Func<TEntity, decimal>>? selector, Expression<Func<TEntity, bool>>? filter = null);

        Task<bool> Exists(Expression<Func<TEntity, bool>> filter);



        Task<int> CountAsync(IEnumerable<Expression<Func<TEntity, bool>>> filters);
        Task<int> CountAsync(Expression<Func<TEntity, bool>>? filter = null);
        Task<int> CountDistinct<TKey>(Expression<Func<TEntity, TKey>> keySelector, IEnumerable<Expression<Func<TEntity, bool>>>? filters = null);



        Task<List<TEntity>> GetAsync<OUT>(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, int skip = 0, int take = -1, params string[] populate);
        Task<TEntity?> GetByAsyncNullable(Expression<Func<TEntity, bool>> filter, params string[] includes);
        Task<List<OUT>> GetAsync<OUT>(Func<IQueryable, IQueryable<OUT>> select, IEnumerable<Expression<Func<TEntity, bool>>>? filters = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, int skip = 0, int take = -1, params string[] populate);
        Task<TKey> Min<TKey>(Expression<Func<TEntity, TKey>> keySelector, IEnumerable<Expression<Func<TEntity, bool>>>? filters = null);
        Task<TKey> Max<TKey>(Expression<Func<TEntity, TKey>> keySelector, IEnumerable<Expression<Func<TEntity, bool>>>? filters = null);
    }
}
