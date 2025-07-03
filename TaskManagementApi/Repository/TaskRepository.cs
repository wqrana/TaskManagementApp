using Microsoft.EntityFrameworkCore;
using System;
using TaskManagementApi.DAL;
using TaskManagementApi.Models;
namespace TaskManagementApi.Repository
{
    public interface ITaskRepository
    {        
            Task<IEnumerable<TaskItem>> GetAllTasksAsync();
            Task<TaskItem> GetTaskByIdAsync(int id);
            Task AddTaskAsync(TaskItem task);
            Task UpdateTaskAsync(TaskItem task);
            Task DeleteTaskAsync(int id);
        
    }
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskItem>> GetAllTasksAsync()
        {
            return await _context.TaskItems.ToListAsync();
        }

        public async Task<TaskItem> GetTaskByIdAsync(int id)
        {
            return await _context.TaskItems.FindAsync(id);
        }

        public async Task AddTaskAsync(TaskItem task)
        {
            task.CreatedAt = DateTime.Now;
            task.UpdatedAt = DateTime.Now;
            task.Category = CategorizeTask(task.Description);
            await _context.TaskItems.AddAsync(task);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTaskAsync(TaskItem task)
        {
            task.UpdatedAt = DateTime.Now;
            task.Category = CategorizeTask(task.Description);
            _context.Entry(task).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(int id)
        {
            var task = await _context.TaskItems.FindAsync(id);
            if (task != null)
            {
                _context.TaskItems.Remove(task);
                await _context.SaveChangesAsync();
            }
        }

        private string CategorizeTask(string description)
        {
            if (string.IsNullOrEmpty(description))
                return "Uncategorized";

            // Simple keyword-based categorization (AI-mocked feature)
            var lowerDesc = description.ToLower();

            if (lowerDesc.Contains("meeting") || lowerDesc.Contains("call"))
                return "Communication";

            if (lowerDesc.Contains("buy") || lowerDesc.Contains("purchase"))
                return "Shopping";

            if (lowerDesc.Contains("write") || lowerDesc.Contains("document"))
                return "Writing";

            if (lowerDesc.Contains("fix") || lowerDesc.Contains("bug"))
                return "Development";

            if (lowerDesc.Contains("learn") || lowerDesc.Contains("study"))
                return "Learning";

            return "General";
        }
    }
}
