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
            Task<TaskItem> AddTaskAsync(TaskItem task);
            Task<TaskItem> UpdateTaskAsync(TaskItem task);
            Task<bool> DeleteTaskAsync(int id);
        
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

        public async Task<TaskItem?> GetTaskByIdAsync(int id)
        {
            var task = await _context.TaskItems.AsNoTracking().
                                        FirstOrDefaultAsync(t => t.Id == id);
            return task;
        }

        public async Task<TaskItem> AddTaskAsync(TaskItem task)
        {
            task.CreatedAt = DateTime.Now;           
            task.Category = CategorizeTask(task.Description);
            await _context.TaskItems.AddAsync(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<TaskItem> UpdateTaskAsync(TaskItem task)
        {
            task.UpdatedAt = DateTime.Now;
            task.Category = CategorizeTask(task.Description);
            _context.Entry(task).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var isDeleted = false;
            var task = await _context.TaskItems.FindAsync(id);
            if (task != null)
            {
                _context.TaskItems.Remove(task);
                await _context.SaveChangesAsync();
                isDeleted = true;
            }
            return isDeleted;
        }
        // mockup for key word matching for Task category
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
