using System;
using System;
using ExpenseTracker.Models;
using ExpenseTracker.Repo;
using ExpenseTracker.Services;
using Moq;

namespace UnitTests.ServiceTests
{
    [TestFixture]
	public class ExpenseCategoryTests
	{

		private Mock<IExpenseCategoryRepository> mockExpensecategoryRepository;

		private IExpenseCategoryService expenseCategoryService;

       

        [SetUp]
        public void SetUp()
        {
            mockExpensecategoryRepository = new Mock<IExpenseCategoryRepository>();

            expenseCategoryService = new ExpenseCategoryService(mockExpensecategoryRepository.Object);

        }


        [Test]
        public async Task AddExpenseCategoryAsync_ValidExpenseCategory_ReturnsAddedExpenseCategory()
        {
            // Arrange
            var expenseCategoryToAdd = new ExpenseCategory
            {
                Id = 1,
                Name = "Food",
                ExpenseId = 1
            };

            var addedExpenseCategory = new ExpenseCategory
            {
                Id = 1,
                Name = "Travel",
                ExpenseId = 1
            };

            mockExpensecategoryRepository
                .Setup(repo => repo.CreateExpenseCategory(expenseCategoryToAdd))
                .ReturnsAsync(addedExpenseCategory);

            // Act
            var result = await expenseCategoryService.AddExpenseCategoryAsync(expenseCategoryToAdd);

            // Assert
            Assert.That(result, Is.EqualTo(addedExpenseCategory));
            mockExpensecategoryRepository.Verify(repo => repo.CreateExpenseCategory(expenseCategoryToAdd), Times.Once);
        }

        [Test]
        public async Task DeleteExpenseCategoryById_ExistingExpenseCategoryId_CallsDeleteMethodInRepository()
        {
            // Arrange
            var expenseCategoryIdToDelete = 1;

            // Act
            await expenseCategoryService.DeleteExpenseCategoryById(expenseCategoryIdToDelete);

            // Assert
            mockExpensecategoryRepository.Verify(repo => repo.DeleteExpenseCategoryById(expenseCategoryIdToDelete), Times.Once);
        }

       

        [Test]
        public async Task FindExpenseCategoryById_ReturnCorrectExpenseCategory()
        {
            //Arrange
            var addedExpenseCategory = new ExpenseCategory
            {
                Id = 1,
                Name = "Travel",
                ExpenseId = 1
            };

            mockExpensecategoryRepository
            .Setup(repo => repo.GetExpenseCategoryById(1))
            .ReturnsAsync(addedExpenseCategory);



            //Act
            var result = await expenseCategoryService.GetExpenseCategoryByIdAsync(1);

            //Assert
            Assert.That(result, Is.EqualTo(addedExpenseCategory));
            mockExpensecategoryRepository.Verify(repo => repo.GetExpenseCategoryById(addedExpenseCategory.Id), Times.Once);

        }

        [Test]
        public async Task UpdateExpense_ReturnUpdatedExpense()

        {

            // Arrange
            var expenseCategoryExisting = new ExpenseCategory
            {
                Id = 1,
                Name = "Food",
                ExpenseId = 1
            };

            var addedExpenseUpdated = new ExpenseCategory
            {
                Id = 1,
                Name = "Travel",
                ExpenseId = 1
            };

            mockExpensecategoryRepository
            .Setup(repo => repo.GetExpenseCategoryById(expenseCategoryExisting.Id))
            .ReturnsAsync(expenseCategoryExisting);


            mockExpensecategoryRepository.Setup(repo => repo.UpdateExpenseCategory(addedExpenseUpdated))
                .ReturnsAsync(addedExpenseUpdated);

            //Act
            var result = await expenseCategoryService.UpdateExpenseCategoryByIdAsync(1, addedExpenseUpdated);

            //Assert
            Assert.That(result, Is.EqualTo(addedExpenseUpdated));

            mockExpensecategoryRepository.Verify(repo => repo.GetExpenseCategoryById(expenseCategoryExisting.Id), Times.Once);

            mockExpensecategoryRepository.Verify(repo => repo.UpdateExpenseCategory(addedExpenseUpdated), Times.Once);


        }


        [Test]
        public async Task GetAllExpenseCategories_ReturnListOfCategories()

        {
            //Arrange
            var expenseCategoryList = new List<ExpenseCategory> { new ExpenseCategory { Id = 1,
                Name = "Food",
                ExpenseId = 1 }, new ExpenseCategory { Id = 2,
                Name = "Travel",
                ExpenseId = 2 } };

            mockExpensecategoryRepository.Setup(repo => repo.GetAllExpenseCategories()).ReturnsAsync(expenseCategoryList);

            //Act
            var result = await expenseCategoryService.GetAllExpenseCategoriesAsync();

            //Assert
            Assert.That(result, Is.EqualTo(expenseCategoryList));
            mockExpensecategoryRepository.Verify(repo => repo.GetAllExpenseCategories(), Times.Once);

        }

        [Test]
        public async Task GetAllExpensesByExpenseCategoryId_ReturnListOfExpenses()
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


            mockExpensecategoryRepository.Setup(repo => repo.GetAllExpensesByCategoryId(1))
                .ReturnsAsync(expenseList);


            //Act
            var result = await expenseCategoryService.GetAllExpensesByCategoryIdAsync(1);

            //Assert
            Assert.That(result, Is.EqualTo(expenseList));
            mockExpensecategoryRepository.Verify(repo => repo.GetAllExpensesByCategoryId(1), Times.Once);


        }



    }
}

