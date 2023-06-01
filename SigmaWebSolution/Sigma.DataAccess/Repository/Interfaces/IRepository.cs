using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sigma.DataAccess.Repository.Interfaces
{    public interface IRepository<T> where T : class
    {
        // we want to pass GetFirstOrDefault an expression with input a function of type T
        // and output boolean, that will return a type T
        T GetFirstOrDefault(Expression<Func<T, bool>> filter, 
            string? includeProperties = null, bool tracked = true);
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, 
            string? includeProperties = null);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);

    }
}
