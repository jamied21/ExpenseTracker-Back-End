
using System;
using ExpenseTracker.Models;
using ExpenseTracker.Repo;
using ExpenseTracker.Services;
using Moq;
namespace UnitTests.ServiceTests
{
    [TestFixture]
    public class UserServiceTests
	{
        private Mock<IUserRepository> mockUserRepository;
		private IUserService userService;

        [SetUp]
        public void SetUp()
        {
            // Initialize a mock repository for each test
            mockUserRepository = new Mock<IUserRepository>();

            // Inject the mock repository into the service
            userService = new UserService(mockUserRepository.Object);
        }

        [Test]
        public async Task AddUserAsync_ValidUser_ReturnsAddedUser()
        {
            // Arrange
            var userToAdd = new User
            {
                Id = 1,
                Username = "JD1234",
                Password = "Password"
               
            };

            var addedUser = new User
            {
                Id = 1,
                Username = "JD1234",
                Password = "Password"
            };

            mockUserRepository
                .Setup(repo => repo.CreateUserAsync(userToAdd))
                .ReturnsAsync(addedUser);

            // Act
            var result = await userService.CreateUserAsync(userToAdd);

            // Assert
            Assert.That(result, Is.EqualTo(addedUser));
            mockUserRepository.Verify(repo => repo.CreateUserAsync(userToAdd), Times.Once);
        }

        [Test]
        public async Task DeleteUserById_ExistingUserId_CallsUserMethodInRepository()
        {
            // Arrange
            var userIdToDelete = 1;

            // Act
            await userService.DeleteUserAsync(userIdToDelete);

            // Assert
            mockUserRepository.Verify(repo => repo.DeleteUserAsync(userIdToDelete), Times.Once);
        }


        [Test]
        public async Task GetAllUsers_ReturnListOfUsers()
        {
            //Arrange
            var userList = new List<User> { new User { Id = 1,
                Username = "JD1234",
                Password = "Password1"
             }, new User {Id = 2,
                Username = "RD1234",
                Password = "Password2" } };


            mockUserRepository.Setup(repo => repo.GetAllUsersAsync())
                .ReturnsAsync(userList);


            //Act
            var result = await userService.GetAllUsersAsync();

            //Assert
            Assert.That(result, Is.EqualTo(userList));
            mockUserRepository.Verify(repo => repo.GetAllUsersAsync(), Times.Once);


        }

        [Test]
        public async Task FindUserById_ReturnCorrectUser()
        {
            //Arrange
            var userToFind = new User
            {
                Id = 1,
                Username = "JD1234",
                Password = "Password1"
            };

            mockUserRepository
            .Setup(repo => repo.GetUserByIdAsync(1))
            .ReturnsAsync(userToFind);



            //Act
            var result = await userService.GetUserByIdAsync(1);

            //Assert
            Assert.That(result, Is.EqualTo(userToFind));
            mockUserRepository.Verify(repo => repo.GetUserByIdAsync(userToFind.Id), Times.Once);

        }

        [Test]
        public async Task UpdateUser_ReturnUpdatedUser()

        {

            //Arrange
            var userExisting = new User
            {
                Id = 1,
                Username = "JD1234",
                Password = "Password1"

            };

            var userToUpdate = new User
            {
                Id = 1,
                Username = "JD1234",
                Password = "Password2"
            };

            mockUserRepository
            .Setup(repo => repo.GetUserByIdAsync(userExisting.Id))
            .ReturnsAsync(userExisting);


            mockUserRepository.Setup(repo => repo.UpdateUserAsync(userToUpdate))
                .ReturnsAsync(userToUpdate);

            //Act
            var result = await userService.UpdateUserAsync(1, userToUpdate);

            //Assert
            Assert.That(result, Is.EqualTo(userToUpdate));

            mockUserRepository.Verify(repo => repo.GetUserByIdAsync(userExisting.Id), Times.Once);

            mockUserRepository.Verify(repo => repo.UpdateUserAsync(userToUpdate), Times.Once);

        }
    }
}

