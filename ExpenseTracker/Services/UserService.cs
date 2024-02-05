using System;
using ExpenseTracker.Models;
using ExpenseTracker.Repo;

namespace ExpenseTracker.Services
{
    public class UserService : IUserService
	{
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            return await userRepository.CreateUserAsync(user);
        }

        public async Task DeleteUserAsync(int userId)
        {
            await userRepository.DeleteUserAsync(userId);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await userRepository.GetAllUsersAsync();
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await userRepository.GetUserByIdAsync(userId);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await userRepository.GetUserByUsernameAsync(username);
        }

        public async Task<User> UpdateUserAsync(int userId, User user)
        {
            var result = await userRepository.GetUserByIdAsync(userId);
            if (result != null)

            {

                return await userRepository.UpdateUserAsync(user);
            }

            return null;
        }
    }
}

