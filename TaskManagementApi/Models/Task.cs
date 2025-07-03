using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagementApi.Models
{
    public enum TaskPriority
    {
        Low,
        Medium,
        High
    }
    [Table("Task")]
    public class TaskItem
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;
        
        public string Description { get; set; } = string.Empty;

        [Required]
        [StringLength(10)]
        public string Priority { get; set; } = string.Empty; // "High", "Medium", "Low"

        public DateTime DueDate { get; set; } = DateTime.Now.AddDays(7);
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

        [StringLength(50)]
        public string Category { get; set; } = string.Empty; // Added for AI categorization
    }
}
