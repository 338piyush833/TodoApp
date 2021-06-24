using System;
using System.Threading.Tasks;
using TodoApp.Data;

namespace TodoApp.IRepository
{

    public interface IDatabaseRepository : IDisposable
    {
        //Here Table repository is Intialize
        IDatabaseTablesRepository<TodoTask> TodoTasks { get; }

        Task Save();
    }
}
