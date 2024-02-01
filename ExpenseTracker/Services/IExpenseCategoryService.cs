using System;
using ExpenseTracker.Models;

namespace ExpenseTracker.Services
{
	public interface IExpenseCategoryService
	{


        Task<IEnumerable<ExpenseCategory>> GetAllExpenseCategoriesAsync();
        Task<ExpenseCategory> GetExpenseCategoryByIdAsync(int categoyId);
        Task DeleteExpenseCategoryById(int categoyId);
        Task<ExpenseCategory> AddExpenseCategoryAsync(ExpenseCategory category);
        Task<ExpenseCategory> UpdateExpenseCategoryByIdAsync(int categoyId, ExpenseCategory category);
        Task<IEnumerable<Expense>> GetAllExpensesByCategoryIdAsync(int categoyId);


    }
}

