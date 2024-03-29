﻿        using System;
        using ExpenseTracker.Models;
        using ExpenseTracker.Services;
        using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;


        namespace ExpenseTracker.Controller
        {
            [ApiController]
            [Route("api/[controller]")]
            public class ExpenseController : ControllerBase
            {
      

        private readonly IExpenseService expenseService;

        public ExpenseController(IExpenseService expenseService)
                {
                    this.expenseService = expenseService;
                   
        }
        #region GetExpenses

                [HttpGet]
                public async Task<ActionResult<IEnumerable<Expense>>> GetExpenses()
                    {
                    try

                    {
                        return Ok(await expenseService.GetAllExpensesAsync());

                    }

                    catch (Exception)

                    {
                        return StatusCode(StatusCodes.Status500InternalServerError,
                            "Error retrieving data from the database");
                    }

                }
        #endregion GetExpenses

        #region GetExpenseById
        [HttpGet("{id:int}")]
                    public async Task<ActionResult<Expense>> GetExpenseById(int id)
                {
                    try
                    {
                        var expense = await expenseService.GetExpenseByIdAsync(id);

                        if (expense == null)
                        {
                            return NotFound($"Expense with Id = {id} not found");
                        }

                        return Ok(expense);


                    }

                    catch (Exception)

                    {
                        return StatusCode(StatusCodes.Status500InternalServerError,
                            "Error retrieving data from the database");
                    }

                }
        #endregion GetExpenseById

        #region CreateExpense
                [HttpPost]
                public async Task<ActionResult<Expense>> CreateExpense(Expense expense)
                {
           

            try {




                //var existingUser = await expenseService.GetUserByIdAsync(expense.UserId);
                //var existingCategory = await expenseService.GetCategoryByIdAsync(expense.CategoryId);

                //if (existingUser == null || existingCategory == null)
                //{
                //    // Handle the case where the user or category is not found
                //    // You might want to return a BadRequest or handle it accordingly
                //    return BadRequest("Invalid userId or categoryId. Make sure they exist in the database.");
                //}

                //// Assign the existing user and category to the expense
                //expense.User = existingUser;
                //expense.Category = existingCategory;

                // Add the expense to the database
                var result = await expenseService.AddExpenseAsync(expense);

                if (result == null)
                {
                      return BadRequest("Error creating expense.");
                }

                return Ok(result);

            }

            catch (Exception)
            {
               
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new expense record");
            }


            }
        #endregion CreateExpense

        #region UpdateExpense

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Expense>> UpdateExpense(int id, Expense expense)
        {
            try {
                    var expenseToUpdate = await expenseService.GetExpenseByIdAsync(id);

                    if (expenseToUpdate == null)

                    {
                        return NotFound($"Expense with Id = {id} not found");

                    }

                if (id != expense.Id)
                {

                    return BadRequest("Expense ID mismatch");
                }

                return Ok(await expenseService.UpdateExpenseByIdAsync(id,expense));

                }


            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }
        #endregion UpdateExpense

        #region DeleteExpense
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Expense>> DeleteExpense(int id)

        {

            try
            {
                var expenseToDelete = expenseService.GetExpenseByIdAsync(id);

                if (expenseToDelete == null)

                {
                    return NotFound($"Expense with Id = {id} not found");

                }

                await expenseService.DeleteExpenseById(id);
                return Accepted();
                
            }

            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }

            
        }


        #endregion DeleteExpense

       
    }
}

