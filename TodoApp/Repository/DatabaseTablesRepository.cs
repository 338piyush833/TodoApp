using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TodoApp.Data;
using TodoApp.IRepository;

namespace TodoApp.Repository
{
    /// <summary>
    /// This class deals with the datbase tables data by using DatabaseContext
    /// </summary>
    /// <typeparam name="T">Table Entity</typeparam>
    public class DatabaseTablesRepository<T> : IDatabaseTablesRepository<T> where T : class
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<T> _dbTable;

        public DatabaseTablesRepository(DatabaseContext context)
        {
            _context = context;
            _dbTable = _context.Set<T>();
        }

        //delete the Item By Id from database table
        public async Task DeleteItem(int id) 
        {
            T entity = await _dbTable.FindAsync(id);
            _dbTable.Remove(entity);
        }

        //delete multiple Items from database table
        public void DeleteMultipleItems(IEnumerable<T> entities)
        {
            _dbTable.RemoveRange(entities);
        }

        //get perticular item with expression
        public async Task<T> GetOneItem(Expression<Func<T, bool>> expression)
        {
            IQueryable<T> query = _dbTable;
            return await query.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        //get all items from database table
        public async Task<IList<T>> GetAllItems()
        {
            IQueryable<T> query = _dbTable;
            return await query.AsNoTracking().ToListAsync();
        }

        //add new item to database table
        public async Task InsertItem(T entity)
        {
            await _dbTable.AddAsync(entity);
        }

        //add multiple new item to database table
        public async Task InsertMultipleItems(IEnumerable<T> entities)
        {
            await _dbTable.AddRangeAsync(entities);
        }

        //update item in database table
        public void UpdateItem(T entitiy)
        {
            _dbTable.Attach(entitiy);
            _context.Entry(entitiy).State = EntityState.Modified;
        }
    }
}
