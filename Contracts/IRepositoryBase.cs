using System.Linq.Expressions;

namespace Contracts
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll(bool trackChanges);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
        Task<T> FindByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges);
        void Create(T entity);
        void CreateRange(List<T> entities);
        void UpdateRange(List<T> entities);
        void Update(T entity);
        void Delete(T entity);
        //Task BeginTransaction(CancellationToken cancellationToken);
        //Task CommitAsync(CancellationToken cancellationToken);
        //Task Rollback(CancellationToken cancellationToken);
        //Task ExecuteFromSqlRaw(string query, CancellationToken cancellationToken, SqlParameterExpression[] parameters = null);
        //IQueryable<T> GetFromSqlRaw(string query, SqlParameterExpression[] parmeters = null);
    }
}
