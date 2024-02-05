using System;
using ExpenseTracker.Models;
using ExpenseTracker.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpenseCategoryController :ControllerBase
	{

		private readonly IExpenseCategoryService expenseCategoryService;

        public ExpenseCategoryController(IExpenseCategoryService expenseCategoryService)
        {
            this.expenseCategoryService = expenseCategoryService;
        }

        #region GetExpenseCategories

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExpenseCategory>>> GetExpenseCategories()
        {
            try

            {
                return Ok(await expenseCategoryService.GetAllExpenseCategoriesAsync());

            }

            catch (Exception)

            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }

        }
        #endregion GetExpenseCategories

        #region GetExpenseCategoryById
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ExpenseCategory>> GetExpenseCategoryById(int id)
        {
            try
            {
                var category = await expenseCategoryService.GetExpenseCategoryByIdAsync(id);

                if (category == null)
                {
                    return NotFound($"Expense with Id = {id} not found");
                }

                return Ok(category);


            }

            catch (Exception)

            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }

        }
        #endregion GetExpenseCategoryById

        #region CreateExpenseCategory
        [HttpPost]
        public async Task<ActionResult<ExpenseCategory>> CreateExpense(ExpenseCategory category)
        {

            try
            {
                var result = await expenseCategoryService.AddExpenseCategoryAsync(category);

                if (result == null)
                {
                    return BadRequest();

                }

                return Ok(result);

            }

            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new category record");
            }


        }
        #endregion CreateExpenseCategory

        #region UpdateExpenseCategory

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ExpenseCategory>> UpdateExpense(int id, ExpenseCategory category)
        {
            try
            {
                var expenseToUpdate = await expenseCategoryService.GetExpenseCategoryByIdAsync(id);

                if (expenseToUpdate == null)

                {
                    return NotFound($"Expense with Id = {id} not found");

                }

                if (id != category.Id)
                {

                    return BadRequest("Expense ID mismatch");
                }

                return Ok(await expenseCategoryService.UpdateExpenseCategoryByIdAsync(id, category));

            }


            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }
        #endregion UpdateExpenseCategory

        #region DeleteExpenseCategory
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Expense>> DeleteExpense(int id)

        {

            try
            {
                var categoryToDelete = expenseCategoryService.GetExpenseCategoryByIdAsync(id);

                if (categoryToDelete == null)

                {
                    return NotFound($"Expense with Id = {id} not found");

                }

                await expenseCategoryService.DeleteExpenseCategoryById(id);
                return Accepted();

            }

            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }


        }


        #endregion DeleteExpenseCategory


        #region GetExpensesByCategoryId

        public async Task<ActionResult<IEnumerable<Expense>>> GetExpensesByCategoryId(int id)

        {
            try
            {
                var category = await expenseCategoryService.GetAllExpensesByCategoryIdAsync(id);

                if (category == null)
                {
                    return NotFound($"Expense with Id = {id} not found");
                }

                return Ok(category);


            }

            catch (Exception)

            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }


        }

        #endregion GetExpensesByCategoryId

    }
}

