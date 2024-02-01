using Moq;
using ExpenseTracker.Models;
using ExpenseTracker.Data;
using ExpenseTracker.Repo;
using Microsoft.EntityFrameworkCore;

namespace UnitTests.RepositoryTests
{
    [TestFixture]
    public class ExpenseRepositoryTests
	{
        [Test]
        public async Task CreateExpense_ShouldCreateNewExpense()
        {
            //// Arrange
            //var expense = new Expense { Name = "TestExpense", Amount = 100.00M };

            //var mockContext = new Mock<ExpenseTrackerContext>();
            //mockContext.Setup(context => context.Expenses.AddAsync(It.IsAny<Expense>(), default))
            //           .ReturnsAsync(new Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Expense>());

            //var repository = new ExpenseRepository(mockContext.Object);

            //// Act
            //var result = await repository.CreateExpense(expense);

            //// Assert
            //Assert.NotNull(result);
            //mockContext.Verify(context => context.SaveChangesAsync(default), Times.Once);
        }

    }
}

