using System;
using ExpenseTracker.Data;
using ExpenseTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Repo
{
	public class ExpenseCategoryRepository: IExpenseCategoryRepository
    {
        private readonly ExpenseTrackerContext _context;

        public ExpenseCategoryRepository(ExpenseTrackerContext context)
        {
            _context = context;
        }

        public async Task<ExpenseCategory> CreateExpenseCategory(ExpenseCategory expenseCategory)
        {
            var result = await _context.ExpenseCategories.AddAsync(expenseCategory);
            await _context.SaveChangesAsync();  
            return result.Entity;
        }

        public async Task DeleteExpenseCategoryById(int id)
        {
            var expenseCategoryToBeDeleted = await _context.ExpenseCategories.FindAsync(id);
           _context.ExpenseCategories.Remove(expenseCategoryToBeDeleted);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ExpenseCategory>> GetAllExpenseCategories()
        {
            return await _context.ExpenseCategories.ToListAsync();
        }

        public async Task<IEnumerable<Expense>> GetAllExpensesByCategoryId(int categoryId)
        {
            return await _context.Expenses
             .Where(expense => expense.CategoryId == categoryId)
             .ToListAsync();
        }

        public async Task<ExpenseCategory> GetExpenseCategoryById(int id)
        {
            return await _context.ExpenseCategories.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ExpenseCategory> UpdateExpenseCategory(ExpenseCategory expenseCategory)
        {
            var result = await _context.ExpenseCategories.FindAsync(expenseCategory.Id);
            if (result != null)

            {
                result.Name = expenseCategory.Name;
                result.ExpenseId = expenseCategory.ExpenseId;
                

                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}

