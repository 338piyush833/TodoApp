using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TodoApp.IRepository
{
    public interface IDatabaseTablesRepository<T> where T : class
    {
        Task<IList<T>> GetAllItems();

        Task<T> GetOneItem(Expression<Func<T, bool>> expression);
        Task InsertItem(T entity);
        Task InsertMultipleItems(IEnumerable<T> entities);
        Task DeleteItem(int id);
        void DeleteMultipleItems(IEnumerable<T> entities);
        void UpdateItem(T entitiy);
    }
}
