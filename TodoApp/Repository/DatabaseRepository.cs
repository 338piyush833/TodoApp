#region Namespaces
using System;
using System.Threading.Tasks;
using TodoApp.Data;
using TodoApp.IRepository;
#endregion

namespace TodoApp.Repository
{
    /// <summary>
    /// This class deals with database and Intialize all database tables context 
    /// </summary>
    public class DatabaseRepository : IDatabaseRepository
    {
        #region Properties
        private readonly DatabaseContext _context;
        private IDatabaseTablesRepository<TodoTask> _todoTask;
        #endregion

        #region Constructor
        public DatabaseRepository(DatabaseContext context)
        {
            _context = context;
        }

        #endregion
        public IDatabaseTablesRepository<TodoTask> TodoTasks => _todoTask ??= new DatabaseTablesRepository<TodoTask>(_context);


        #region Methods
        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
        #endregion
    }
}
