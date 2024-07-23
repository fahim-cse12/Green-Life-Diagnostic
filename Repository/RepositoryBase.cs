using Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected RepositoryContext RepositoryContext;
        public RepositoryBase(RepositoryContext repositoryContext) => RepositoryContext = repositoryContext;
        public IQueryable<T> FindAll(bool trackChanges) => !trackChanges ? RepositoryContext.Set<T>().AsNoTracking() : RepositoryContext.Set<T>();
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
            !trackChanges ? RepositoryContext.Set<T>().Where(expression).AsNoTracking() : RepositoryContext.Set<T>().Where(expression);
        public void Create(T entity) => RepositoryContext.Set<T>().Add(entity);
        public void Update(T entity) => RepositoryContext.Set<T>().Update(entity);
        public void Delete(T entity) => RepositoryContext.Set<T>().Remove(entity);

        //public void Rollback()
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task ExecuteFromSqlRaw(string query, CancellationToken cancellationToken, SqlParameterExpression[] parameters = null)
        //{
        //    await RepositoryContext.Database.ExecuteSqlRawAsync(query, cancellationToken, parameters);
        //}

        //public IQueryable<T> GetFromSqlRaw(string query, SqlParameterExpression[] parmeters = null)
        //{
        //    return RepositoryContext.Set<T>().FromSqlRaw(query, parmeters);
        //}

       
        //public async Task Rollback(CancellationToken cancellationToken)
        //{
        //    await _transaction.RollbackAsync(cancellationToken);
        //    await _transaction.DisposeAsync();
        //}
    }
}
