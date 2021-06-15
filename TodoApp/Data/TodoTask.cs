using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApp.Data
{
    public class TodoTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string description { get; set; }
        public string status { get; set; }

    }
}
