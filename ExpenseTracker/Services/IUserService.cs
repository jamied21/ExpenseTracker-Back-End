using ExpenseTracker.Models;

namespace ExpenseTracker.Services
{
    public interface IUserService
    {

        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int userId);
        Task<User> GetUserByUsernameAsync(string username);
        Task<User> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(int userId, User user);
        Task DeleteUserAsync(int userId);
    }
}