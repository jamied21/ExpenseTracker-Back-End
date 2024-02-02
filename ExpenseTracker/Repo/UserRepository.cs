using System;
using ExpenseTracker.Data;
using ExpenseTracker.Models;

namespace ExpenseTracker.Repo
{
	public class UserRepository: IUserRepository
	{
		private readonly ExpenseTrackerContext _context;

        public UserRepository(ExpenseTrackerContext context)
        {
            _context = context;
        }

        public Task<int> AddUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUserAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByUsernameAsync(string username)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}

