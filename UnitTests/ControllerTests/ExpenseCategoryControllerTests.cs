using System;
using ExpenseTracker.Controller;
using ExpenseTracker.Models;
using ExpenseTracker.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace UnitTests.ControllerTests
{
	[TestFixture]
	public class ExpenseCategoryControllerTests
	{
		private Mock<IExpenseCategoryService> mockService;
		private ExpenseCategoryController expenseCategoryController;

		[SetUp]
		public void SetUp()
		{
			mockService = new Mock<IExpenseCategoryService>();
			expenseCategoryController = new ExpenseCategoryController(mockService.Object);

		}


        [Test]
        public async Task GetExpenseCategories_ReturnsOkResultWithExpenseCategories()
        {
            //Arrange
            var expenseCategoryList = new List<ExpenseCategory> { new ExpenseCategory { Id = 1,
                Name = "Alcohol", ExpenseId = 1

                }, new ExpenseCategory {Id = 2,
                Name = "Food", ExpenseId =2
                 } };

            mockService.Setup(service => service.GetAllExpenseCategoriesAsync()).ReturnsAsync(expenseCategoryList);

            // Act
            var result = await expenseCategoryController.GetExpenseCategories();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult.Value, Is.InstanceOf<List<ExpenseCategory>>());
            Assert.That(okResult.Value, Is.EqualTo(expenseCategoryList));
            mockService.Verify(service => service.GetAllExpenseCategoriesAsync(), Times.Once);
        }

        [Test]
        public async Task GetExpensesCategory_Exception_ReturnsStatusCode500()
        {
            // Arrange
            mockService.Setup(service => service.GetAllExpenseCategoriesAsync()).ThrowsAsync(new Exception("Simulated error"));

            // Act
            var result = await expenseCategoryController.GetExpenseCategories();

            // Assert
            Assert.IsInstanceOf<ObjectResult>(result.Result);
            var objectResult = result.Result as ObjectResult;
            Assert.That(objectResult.StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
            Assert.That(objectResult.Value, Is.EqualTo("Error retrieving data from the database"));
        }

        [Test]
        public async Task GetExpenseCategoryById_ExistingExpenseCategory_ReturnsOkResultWithExpenseCategory()
        {
            // Arrange
            int expenseCategoryId = 1;
            var expectedExpenseCategory = new ExpenseCategory
            {
                Id = 1,
                Name = "Alcohol",
                ExpenseId = 1
            };

            mockService.Setup(service => service.GetExpenseCategoryByIdAsync(expenseCategoryId)).ReturnsAsync(expectedExpenseCategory);

            // Act
            var result = await expenseCategoryController.GetExpenseCategoryById(expenseCategoryId);

            // Assert
            Assert.IsNotNull(result.Result);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            mockService.Verify(service => service.GetExpenseCategoryByIdAsync(expenseCategoryId), Times.Once);



            var okResult = result.Result as OkObjectResult;

            Assert.That(okResult.Value, Is.InstanceOf<ExpenseCategory>());
            Assert.That((okResult.Value as ExpenseCategory)?.Id, Is.EqualTo(expectedExpenseCategory.Id));
            Assert.That((okResult.Value as ExpenseCategory)?.Name, Is.EqualTo(expectedExpenseCategory.Name));

        }

        [Test]
        public async Task GetExpenseCategoryById_NonExistingExpense_ReturnsNotFoundResult()
        {
            // Arrange
            int nonExistingId = 99;
            mockService.Setup(service => service.GetExpenseCategoryByIdAsync(nonExistingId)).ReturnsAsync(null as ExpenseCategory);

            // Act
            var result = await expenseCategoryController.GetExpenseCategoryById(nonExistingId);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result.Result);
            var notFoundResult = result.Result as NotFoundObjectResult;
            Assert.AreEqual($"Expense with Id = {nonExistingId} not found", notFoundResult.Value);
        }

        [Test]
        public async Task GetExpenseCategoryById_Exception_ReturnsStatusCode500()
        {
            // Arrange
            int categoryId = 1;
            mockService.Setup(service => service.GetExpenseCategoryByIdAsync(categoryId)).ThrowsAsync(new Exception("Simulated error"));

            // Act
            var result = await expenseCategoryController.GetExpenseCategoryById(categoryId);

            // Assert
            Assert.IsInstanceOf<ObjectResult>(result.Result);
            var objectResult = result.Result as ObjectResult;
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
            Assert.AreEqual("Error retrieving data from the database", objectResult.Value);
        }

        [Test]
        public async Task CreateExpenseCategory_ValidExpenseCategory_ReturnsOkResultWithCreatedExpenseCategory()
        {
            // Arrange
            var newCategory = new ExpenseCategory { Name = "Alcohol", ExpenseId = 1 };
            var createdExpenseCategory = new ExpenseCategory
            {
                Id = 1,
                Name = "Alcohol",
                ExpenseId = 1
            };

            mockService.Setup(service => service.AddExpenseCategoryAsync(newCategory)).ReturnsAsync(createdExpenseCategory);

            // Act
            var result = await expenseCategoryController.CreateExpense(newCategory);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsInstanceOf<ExpenseCategory>(okResult.Value);
            Assert.AreEqual(createdExpenseCategory, okResult.Value);
        }

        [Test]
        public async Task CreateExpenseCategory_InvalidExpenseCategory_ReturnsBadRequestResult()
        {
            // Arrange
            var invalidExpenseCategory = new ExpenseCategory(); // Missing required properties
            mockService.Setup(service => service.AddExpenseCategoryAsync(invalidExpenseCategory)).ReturnsAsync(null as ExpenseCategory);

            // Act
            var result = await expenseCategoryController.CreateExpense(invalidExpenseCategory);

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(result.Result);
        }

        [Test]
        public async Task CreateExpenseCategory_Exception_ReturnsStatusCode500()
        {
            // Arrange
            var newExpenseCategoy = new ExpenseCategory
            {
                Id = 1,
                Name = "Alcohol",
                ExpenseId = 1
            };

            mockService.Setup(service => service.AddExpenseCategoryAsync(newExpenseCategoy)).ThrowsAsync(new Exception("Simulated error"));

            // Act
            var result = await expenseCategoryController.CreateExpense(newExpenseCategoy);

            // Assert
            Assert.IsInstanceOf<ObjectResult>(result.Result);
            var objectResult = result.Result as ObjectResult;
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
            Assert.AreEqual("Error creating new category record", objectResult.Value);
        }

        [Test]
        public async Task UpdateExpense_ExistingExpense_ReturnsOkResultWithUpdatedExpense()
        {
            // Arrange
            int expenseId = 1;
            var existingExpenseCategoy = new ExpenseCategory
            {
                Id = 1,
                Name = "Existing Expense Category",
                ExpenseId = 1
            };
            var updatedExpenseCategoy = new ExpenseCategory
            {
                Id = 1,
                Name = "Updated Expense Category",
                ExpenseId = 1
            };

            mockService.Setup(service => service.GetExpenseCategoryByIdAsync(expenseId)).ReturnsAsync(existingExpenseCategoy);
            mockService.Setup(service => service.UpdateExpenseCategoryByIdAsync(expenseId, updatedExpenseCategoy)).ReturnsAsync(updatedExpenseCategoy);

            // Act
            var result = await expenseCategoryController.UpdateExpense(expenseId, updatedExpenseCategoy);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            mockService.Verify(service => service.GetExpenseCategoryByIdAsync(expenseId), Times.Once);
            mockService.Verify(service => service.UpdateExpenseCategoryByIdAsync(expenseId, updatedExpenseCategoy), Times.Once);


        }

    }
}

