using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models
{
    public class CreateTodoTaskDTO
    {
        [Required]
        [StringLength(maximumLength:50,ErrorMessage = "Title Is Too Long")]
        public string Title { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        [StringLength(maximumLength: 10, ErrorMessage = "status Is Too Long")]
        public string status { get; set; }
    }
    public class TodoTaskDTO : CreateTodoTaskDTO
    {
        public int Id { get; set; }
        
    }
}
