using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.dto;

namespace TaskManager.Services
{
    public class UserService : IUserService
    {
        private readonly TaskManagerContext _context;

        public UserService(TaskManagerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _context.Users.Include(u => u.Role).ToListAsync();
            return users.Select(u => u.ToDto());
        }

        public async Task<UserDto?> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == id);
            return user?.ToDto();
        }
    }

}
