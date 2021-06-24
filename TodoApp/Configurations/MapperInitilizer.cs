using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Data;
using TodoApp.Models;

namespace TodoApp.Configurations
{
    public class MapperInitilizer : Profile
    {
        public MapperInitilizer()
        {
            CreateMap<TodoTask, TodoTaskDTO>().ReverseMap();
            CreateMap<TodoTask, CreateTodoTaskDTO>().ReverseMap();
        }
    }
}
