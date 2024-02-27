using System;
using ExpenseTracker.Models;
using ExpenseTracker.Repo;
using ExpenseTracker.Services;
using Moq;


namespace UnitTests.ServiceTests
{
    [TestFixture]
    public class ExpenseServiceTests
	{
        private Mock<IExpenseRepository> mockExpenseRepository;
        private IExpenseService expenseService;

        [SetUp]
        public void SetUp()
        {
            // Initialize a mock repository for each test
            mockExpenseRepository = new Mock<IExpenseRepository>();

            // Inject the mock repository into the service
            expenseService = new ExpenseService(mockExpenseRepository.Object);
        }

        [Test]
        public async Task AddExpenseAsync_ValidExpense_ReturnsAddedExpense()
        {
            // Arrange
            var expenseToAdd = new Expense
            {
                Id = 1,
                Name = "Test Expense",
                Amount = 100.00M,
                ExpenseDate = DateTime.Now,
                //UserId = 1,
                CategoryId = 1
            };

            var addedExpense = new Expense
            {
                Id = 1,
                Name = "Test Expense",
                Amount = 100.00M,
                ExpenseDate = DateTime.Now,
               // UserId = 1,
                CategoryId = 1
            };

            mockExpenseRepository
                .Setup(repo => repo.CreateExpense(expenseToAdd))
                .ReturnsAsync(addedExpense);

            // Act
            var result = await expenseService.AddExpenseAsync(expenseToAdd);

            // Assert
            Assert.That(result, Is.EqualTo(addedExpense));
            mockExpenseRepository.Verify(repo => repo.CreateExpense(expenseToAdd), Times.Once);
        }

        [Test]
        public async Task DeleteExpenseById_ExistingExpenseId_CallsDeleteMethodInRepository()
        {
            // Arrange
            var expenseIdToDelete = 1;

            // Act
            await expenseService.DeleteExpenseById(expenseIdToDelete);

            // Assert
            mockExpenseRepository.Verify(repo => repo.DeleteExpenseById(expenseIdToDelete), Times.Once);
        }

        [Test]
        public async Task GetAllExpenseCategoriesAsync_ReturnsListOfExpenseCategories()
        {
            // Arrange
            var expenseCategories = new List<ExpenseCategory> { new ExpenseCategory { Id = 1, Name = "Category 1" },
            new ExpenseCategory { Id = 2, Name = "Category 2" } };

            mockExpenseRepository
                .Setup(repo => repo.GetAllExpenseCategoriesAsync())
                .ReturnsAsync(expenseCategories);

            // Act
            var result = await expenseService.GetAllExpenseCategoriesAsync();

            // Assert
            Assert.That(result, Is.EqualTo(expenseCategories));
            mockExpenseRepository.Verify(repo => repo.GetAllExpenseCategoriesAsync(), Times.Once);

        }

        [Test]
        public async Task GetAllExpenses_ReturnListOfExpenses()
        {
            //Arrange
            var expenseList = new List<Expense> { new Expense { Id = 1,
                Name = "Beer",
                Amount = 30.00M,
                ExpenseDate = DateTime.Now,
             //   UserId = 1,
                CategoryId = 1}, new Expense {Id = 2,
                Name = "Food",
                Amount = 15.00M,
                ExpenseDate = DateTime.Now,
              //  UserId = 1,
                CategoryId = 1 } };

           
            mockExpenseRepository.Setup(repo => repo.GetExpenses())
                .ReturnsAsync(expenseList);


            //Act
            var result = await expenseService.GetAllExpensesAsync();

            //Assert
            Assert.That(result, Is.EqualTo(expenseList));
            mockExpenseRepository.Verify(repo => repo.GetExpenses(), Times.Once);


        }

        [Test]
        public async Task FindExpenseById_ReturnCorrectExpense()
        {
            //Arrange
            var expenseToFind = new Expense {
                Id = 1,
                Name = "Beer",
                Amount = 30.00M,
                ExpenseDate = DateTime.Now,
             //   UserId = 1,
                CategoryId = 1
            };

            mockExpenseRepository
            .Setup(repo => repo.GetExpenseById(1))
            .ReturnsAsync(expenseToFind);



            //Act
            var result = await expenseService.GetExpenseByIdAsync(1);

            //Assert
            Assert.That(result, Is.EqualTo(expenseToFind));
            mockExpenseRepository.Verify(repo => repo.GetExpenseById(expenseToFind.Id), Times.Once);

        }

        [Test]
        public async Task UpdateExpense_ReturnUpdatedExpense()

        {

            //Arrange
            var expenseExisting = new Expense
            {
                Id = 1,
                Name = "Beer",
                Amount = 20.00M,
                ExpenseDate = DateTime.Now,
            //    UserId = 1,
                CategoryId = 1
            };
            var expenseUpdated = new Expense
            {
                Id = 1,
                Name = "Beer",
                Amount = 40.00M,
                ExpenseDate = DateTime.Now,
            //    UserId = 1,
                CategoryId = 1
            };

            mockExpenseRepository
            .Setup(repo => repo.GetExpenseById(expenseExisting.Id))
            .ReturnsAsync(expenseExisting);


            mockExpenseRepository.Setup(repo => repo.UpdateExpense(expenseUpdated))
                .ReturnsAsync(expenseUpdated);

            //Act
            var result = await expenseService.UpdateExpenseByIdAsync(1,expenseUpdated);

            //Assert
            Assert.That(result, Is.EqualTo(expenseUpdated));
           
           mockExpenseRepository.Verify(repo => repo.GetExpenseById(expenseExisting.Id), Times.Once);
          
            mockExpenseRepository.Verify(repo => repo.UpdateExpense(expenseUpdated), Times.Once);


        }


    }
}

