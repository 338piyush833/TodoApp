using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Data;
using TodoApp.IRepository;

namespace TodoApp.Repository
{
    public class DatabaseRepository : IDatabaseRepository
    {
        private readonly DatabaseContext _context;

        private IDatabaseTablesRepository<TodoTask> _todoTask;
        public DatabaseRepository(DatabaseContext context)
        {
            _context = context;
        }
        public IDatabaseTablesRepository<TodoTask> TodoTasks => _todoTask ??= new DatabaseTablesRepository<TodoTask>(_context); 

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }


        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
