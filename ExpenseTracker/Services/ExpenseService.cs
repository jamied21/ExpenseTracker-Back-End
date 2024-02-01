using System;
using ExpenseTracker.Models;
using ExpenseTracker.Repo;

namespace ExpenseTracker.Services
{
	public class ExpenseService : IExpenseService
	{
		private readonly IExpenseRepository expenseRepository;

        public ExpenseService(IExpenseRepository expenseRepository)
        {
            this.expenseRepository = expenseRepository;
        }

        public async Task<Expense> AddExpenseAsync(Expense expense)
        {
             return await expenseRepository.CreateExpense(expense);
            
        }

        public async Task DeleteExpenseById(int expenseId)
        {

            await expenseRepository.DeleteExpenseById(expenseId);

        }

        public async Task<IEnumerable<ExpenseCategory>> GetAllExpenseCategoriesAsync()
        {
            return await expenseRepository.GetAllExpenseCategoriesAsync();
        }

        public async Task<IEnumerable<Expense>> GetAllExpensesAsync()
        {
            return await expenseRepository.GetExpenses();
        }

        public async Task<Expense> GetExpenseByIdAsync(int expenseId)
        {
            return await expenseRepository.GetExpenseById(expenseId);
        }

        public async Task<Expense> UpdateExpenseByIdAsync(int id, Expense expense)
        {
            var expenseToUpdate = await expenseRepository.GetExpenseById(id);

            if (expenseToUpdate != null)
            {
                return await expenseRepository.UpdateExpense(expense);
            }

            return null;
        }
    }
}

