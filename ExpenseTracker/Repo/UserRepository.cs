using System;
using ExpenseTracker.Data;
using ExpenseTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Repo
{
	public class UserRepository: IUserRepository
	{
		private readonly ExpenseTrackerContext _context;

        public UserRepository(ExpenseTrackerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            var result = await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            var result = await _context.Users.FindAsync(user.Id);
            if (result != null)

            {
                result.Username = user.Username;
                result.Password = user.Password;
               
                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }

        public async Task DeleteUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}

