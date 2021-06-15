using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Data;

namespace TodoApp.IRepository
{
    public interface IDatabaseRepository : IDisposable
    {
        //Here Table repository is initialised
        IDatabaseTablesRepository<TodoTask> TodoTasks { get; }

        Task Save();
    }
}
