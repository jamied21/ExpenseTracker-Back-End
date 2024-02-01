using System;
using ExpenseTracker.Models;
using ExpenseTracker.Repo;

namespace ExpenseTracker.Services
{
	public class ExpenseCategoryService: IExpenseCategoryService
    {
        private readonly IExpenseCategoryRepository expenseCategoryRepository;

        public ExpenseCategoryService(IExpenseCategoryRepository expenseCategoryRepository)
        {
            this.expenseCategoryRepository = expenseCategoryRepository;
        }

        public async Task<ExpenseCategory> AddExpenseCategoryAsync(ExpenseCategory category)
        {
            return await expenseCategoryRepository.CreateExpenseCategory(category);
        }

        public async Task DeleteExpenseCategoryById(int categoyId)
        {
            await expenseCategoryRepository.DeleteExpenseCategoryById(categoyId);
        }

        public async Task<IEnumerable<ExpenseCategory>> GetAllExpenseCategoriesAsync()
        {
            return await expenseCategoryRepository.GetAllExpenseCategories();
        }

        public async Task<IEnumerable<Expense>> GetAllExpensesByCategoryIdAsync(int categoyId)
        {
            return await expenseCategoryRepository.GetAllExpensesByCategoryId(categoyId);
        }

        public async Task<ExpenseCategory> GetExpenseCategoryByIdAsync(int categoyId)
        {
            return await expenseCategoryRepository.GetExpenseCategoryById(categoyId);
        }

        public async Task<ExpenseCategory> UpdateExpenseCategoryByIdAsync(int categoyId, ExpenseCategory category)
        {
            var result = await expenseCategoryRepository.GetExpenseCategoryById(categoyId);

            if (result != null)

            {

                return await expenseCategoryRepository.UpdateExpenseCategory(category);

            }

            return null;
        }
    }
}

