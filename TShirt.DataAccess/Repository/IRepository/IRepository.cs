using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TShirt.DataAccess.Repository.IRepository
{
    //generic repository where T is a class like Category or Order
    //Update methods are specific to each class so not to include in generic repo
    public interface IRepository<T> where T : class
    {
        T GetFirstOrDefault(Expression<Func<T,bool>> filter, string? includeProperties = null); 
        IEnumerable<T> GetAll(string? includeProperties = null);    
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);

    }
}
