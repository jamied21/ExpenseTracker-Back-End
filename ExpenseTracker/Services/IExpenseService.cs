using System;
using ExpenseTracker.Models;

namespace ExpenseTracker.Services
{
	public interface IExpenseService
	{

        Task<IEnumerable<Expense>> GetAllExpensesAsync();
        Task<Expense> GetExpenseByIdAsync(int expenseId);
        Task DeleteExpenseById(int expenseId);
        Task<Expense> AddExpenseAsync(Expense expense);
        Task<Expense> UpdateExpenseByIdAsync(int id, Expense expense);

        Task<IEnumerable<ExpenseCategory>> GetAllExpenseCategoriesAsync();

        Task<User> GetUserByIdAsync(int? userId);
        Task<ExpenseCategory> GetCategoryByIdAsync(int? categoryId);
      
    }
}

