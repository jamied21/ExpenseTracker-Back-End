using System;
using ExpenseTracker.Models;

namespace ExpenseTracker.Repo
{
	public interface IExpenseRepository
	{
		Task<IEnumerable<Expense>> GetExpenses();
		Task<Expense> GetExpenseById(int id);
		Task<IEnumerable<ExpenseCategory>> GetAllExpenseCategoriesAsync();
        Task DeleteExpenseById(int id);
		Task<Expense> CreateExpense(Expense expense);
		Task<Expense> UpdateExpense(Expense expense);

	}
}

