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
    public class ExpenseControllerTests
    {
        private Mock<IExpenseService> mockExpenseService;
        private ExpenseController expenseController;

        [SetUp]
        public void SetUp()
        {
            mockExpenseService = new Mock<IExpenseService>();
            expenseController = new ExpenseController(mockExpenseService.Object);
        }


        [Test]
        public async Task GetExpenses_ReturnsOkResultWithExpenses()
        {
            //Arrange
            var expenseList = new List<Expense> { new Expense { Id = 1,
                Name = "Beer",
                Amount = 30.00M,
                ExpenseDate = DateTime.Now,
                UserId = 1,
                CategoryId = 1}, new Expense {Id = 2,
                Name = "Food",
                Amount = 15.00M,
                ExpenseDate = DateTime.Now,
                UserId = 1,
                CategoryId = 1 } };

            mockExpenseService.Setup(service => service.GetAllExpensesAsync()).ReturnsAsync(expenseList);

            // Act
            var result = await expenseController.GetExpenses();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult.Value, Is.InstanceOf<List<Expense>>());
            Assert.That(okResult.Value, Is.EqualTo(expenseList));
        }

        [Test]
        public async Task GetExpenses_Exception_ReturnsStatusCode500()
        {
            // Arrange
            mockExpenseService.Setup(service => service.GetAllExpensesAsync()).ThrowsAsync(new Exception("Simulated error"));

            // Act
            var result = await expenseController.GetExpenses();

            // Assert
            Assert.IsInstanceOf<ObjectResult>(result.Result);
            var objectResult = result.Result as ObjectResult;
            Assert.That(objectResult.StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
            Assert.That(objectResult.Value, Is.EqualTo("Error retrieving data from the database"));
        }

        [Test]
        public async Task GetExpenseById_ExistingExpense_ReturnsOkResultWithExpense()
        {
            // Arrange
            int expenseId = 1;
            var expectedExpense = new Expense
            {
                Id = 1,
                Name = "Beer",
                Amount = 30.00M,
                ExpenseDate = DateTime.Now,
                UserId = 1,
                CategoryId = 1
            };

            mockExpenseService.Setup(service => service.GetExpenseByIdAsync(expenseId)).ReturnsAsync(expectedExpense);

            // Act
            var result = await expenseController.GetExpenseById(expenseId);

            // Assert
            Assert.IsNotNull(result.Result);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            mockExpenseService.Verify(service => service.GetExpenseByIdAsync(expenseId), Times.Once);



            //var okResult = result.Result as OkObjectResult;

            //Assert.That(okResult.Value, Is.InstanceOf<Expense>());
            //Assert.That((okResult.Value as Expense)?.Id, Is.EqualTo(expectedExpense.Id));
            //Assert.That((okResult.Value as Expense)?.Name, Is.EqualTo(expectedExpense.Name));

        }

        [Test]
        public async Task GetExpenseById_NonExistingExpense_ReturnsNotFoundResult()
        {
            // Arrange
            int nonExistingExpenseId = 99;
            mockExpenseService.Setup(service => service.GetExpenseByIdAsync(nonExistingExpenseId)).ReturnsAsync(null as Expense);

            // Act
            var result = await expenseController.GetExpenseById(nonExistingExpenseId);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result.Result);
            var notFoundResult = result.Result as NotFoundObjectResult;
            Assert.AreEqual($"Expense with Id = {nonExistingExpenseId} not found", notFoundResult.Value);
        }

        [Test]
        public async Task GetExpenseById_Exception_ReturnsStatusCode500()
        {
            // Arrange
            int expenseId = 1;
            mockExpenseService.Setup(service => service.GetExpenseByIdAsync(expenseId)).ThrowsAsync(new Exception("Simulated error"));

            // Act
            var result = await expenseController.GetExpenseById(expenseId);

            // Assert
            Assert.IsInstanceOf<ObjectResult>(result.Result);
            var objectResult = result.Result as ObjectResult;
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
            Assert.AreEqual("Error retrieving data from the database", objectResult.Value);
        }

        [Test]
        public async Task CreateExpense_ValidExpense_ReturnsOkResultWithCreatedExpense()
        {
            // Arrange
            var newExpense = new Expense { Name = "New Expense", Amount = 50.00M };
            var createdExpense = new Expense { Id = 1, Name = "New Expense", Amount = 50.00M };
            mockExpenseService.Setup(service => service.AddExpenseAsync(newExpense)).ReturnsAsync(createdExpense);

            // Act
            var result = await expenseController.CreateExpense(newExpense);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsInstanceOf<Expense>(okResult.Value);
            Assert.AreEqual(createdExpense, okResult.Value);
        }

        [Test]
        public async Task CreateExpense_InvalidExpense_ReturnsBadRequestResult()
        {
            // Arrange
            var invalidExpense = new Expense(); // Missing required properties
            mockExpenseService.Setup(service => service.AddExpenseAsync(invalidExpense)).ReturnsAsync(null as Expense);

            // Act
            var result = await expenseController.CreateExpense(invalidExpense);

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(result.Result);
        }

        [Test]
        public async Task CreateExpense_Exception_ReturnsStatusCode500()
        {
            // Arrange
            var newExpense = new Expense { Name = "New Expense", Amount = 50.00M };
            mockExpenseService.Setup(service => service.AddExpenseAsync(newExpense)).ThrowsAsync(new Exception("Simulated error"));

            // Act
            var result = await expenseController.CreateExpense(newExpense);

            // Assert
            Assert.IsInstanceOf<ObjectResult>(result.Result);
            var objectResult = result.Result as ObjectResult;
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
            Assert.AreEqual("Error creating new employee record", objectResult.Value);
        }

        [Test]
        public async Task UpdateExpense_ExistingExpense_ReturnsOkResultWithUpdatedExpense()
        {
            // Arrange
            int expenseId = 1;
            var existingExpense = new Expense { Id = expenseId, Name = "Existing Expense", Amount = 75.00M };
            var updatedExpense = new Expense { Id = expenseId, Name = "Updated Expense", Amount = 100.00M };
            mockExpenseService.Setup(service => service.GetExpenseByIdAsync(expenseId)).ReturnsAsync(existingExpense);
            mockExpenseService.Setup(service => service.UpdateExpenseByIdAsync(expenseId, updatedExpense)).ReturnsAsync(updatedExpense);

            // Act
            var result = await expenseController.UpdateExpense(expenseId, updatedExpense);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            mockExpenseService.Verify(service => service.GetExpenseByIdAsync(expenseId), Times.Once);
            mockExpenseService.Verify(service => service.UpdateExpenseByIdAsync(expenseId, updatedExpense), Times.Once);


        }



    }
}

