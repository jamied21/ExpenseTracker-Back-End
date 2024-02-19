using System;
using ExpenseTracker.Data;
using ExpenseTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Repo
{
	public class ExpenseRepository : IExpenseRepository
    {


        private readonly ExpenseTrackerContext _context;

        public ExpenseRepository(ExpenseTrackerContext context)
        {
            _context = context;
        }

        public async Task<Expense> CreateExpense(Expense expense)
        {

            var result = await _context.Expenses.AddAsync(expense);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteExpenseById(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
        }

        public async Task<Expense> GetExpenseById(int id)
        {
            return await _context.Expenses.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Expense>> GetExpenses()
        {
            return await _context.Expenses.ToListAsync();
        }

        public async Task<Expense> UpdateExpense(Expense expense)
        {
            var result = await _context.Expenses.FindAsync(expense.Id);
            if (result != null)

            {
                result.Name = expense.Name;
                result.Amount = expense.Amount;
                result.ExpenseDate = expense.ExpenseDate;
                result.UserId = expense.UserId;
                result.CategoryId = expense.CategoryId;

                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }

        public async Task<IEnumerable<ExpenseCategory>> GetAllExpenseCategoriesAsync()
        {
            return await _context.ExpenseCategories.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int? userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<ExpenseCategory> GetCategoryByIdAsync(int? categoryId)
        {
           return await _context.ExpenseCategories.FindAsync(categoryId);
        }
    }
}

