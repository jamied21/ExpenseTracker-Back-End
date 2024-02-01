using ExpenseTracker.Models;

namespace ExpenseTracker.Repo
{
    public interface IExpenseCategoryRepository
    {
     
        Task<ExpenseCategory> GetExpenseCategoryById(int id);
        Task<IEnumerable<ExpenseCategory>> GetAllExpenseCategories();
        Task DeleteExpenseCategoryById(int id);
        Task<ExpenseCategory> CreateExpenseCategory(ExpenseCategory expenseCategory);
        Task<ExpenseCategory> UpdateExpenseCategory(ExpenseCategory expenseCategory);
        Task<IEnumerable<Expense>> GetAllExpensesByCategoryId(int id);
    }
}