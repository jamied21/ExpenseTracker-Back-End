
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
    public class UserControllerTests
    {
        private Mock<IUserService> mockService;
        private UserController userController;

        [SetUp]
        public void SetUp()
        {
            mockService = new Mock<IUserService>();

            userController = new UserController(mockService.Object);

        }


        [Test]
        public async Task GetUser_ReturnsOkResultWithUsers()
        {
            //Arrange
            var userList = new List<User> { new User { Id = 1,
                Username = "JD1234",
                Password = "Password1"
             }, new User {Id = 2,
                Username = "RD1234",
                Password = "Password2" } };


            mockService.Setup(service => service.GetAllUsersAsync()).ReturnsAsync(userList);

            // Act
            var result = await userController.GetUsers();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult.Value, Is.InstanceOf<List<User>>());
            Assert.That(okResult.Value, Is.EqualTo(userList));
            mockService.Verify(service => service.GetAllUsersAsync(), Times.Once);

        }

        [Test]
        public async Task GetUsers_Exception_ReturnsStatusCode500()
        {
            // Arrange
            mockService.Setup(service => service.GetAllUsersAsync()).ThrowsAsync(new Exception("Simulated error"));

            // Act
            var result = await userController.GetUsers();

            // Assert
            Assert.IsInstanceOf<ObjectResult>(result.Result);
            var objectResult = result.Result as ObjectResult;
            Assert.That(objectResult.StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
            Assert.That(objectResult.Value, Is.EqualTo("Error retrieving data from the database"));
        }

        [Test]
        public async Task GetUserById_ExistingUser_ReturnsOkResultWithUser()
        {
            // Arrange
            int userId = 1;
            var expectedUser = new User
            {
                Id = 1,
                Username = "JD1234",
                Password = "Password1"
            };

            mockService.Setup(service => service.GetUserByIdAsync(userId)).ReturnsAsync(expectedUser);

            // Act
            var result = await userController.GetUserById(userId);

            // Assert
            Assert.IsNotNull(result.Result);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            mockService.Verify(service => service.GetUserByIdAsync(userId), Times.Once);



            var okResult = result.Result as OkObjectResult;

            Assert.That(okResult.Value, Is.InstanceOf<User>());
            Assert.That((okResult.Value as User)?.Id, Is.EqualTo(expectedUser.Id));
            Assert.That((okResult.Value as User)?.Username, Is.EqualTo(expectedUser.Username));

        }

        [Test]
        public async Task GetUserById_NonExistingUser_ReturnsNotFoundResult()
        {
            // Arrange
            int nonExistingUserId = 99;
            mockService.Setup(service => service.GetUserByIdAsync(nonExistingUserId)).ReturnsAsync(null as User);

            // Act
            var result = await userController.GetUserById(nonExistingUserId);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result.Result);
            var notFoundResult = result.Result as NotFoundObjectResult;
            Assert.That(notFoundResult.Value, Is.EqualTo($"User with Id = {nonExistingUserId} not found"));
        }

        [Test]
        public async Task GetUserById_Exception_ReturnsStatusCode500()
        {
            // Arrange
            int userId = 1;
            mockService.Setup(service => service.GetUserByIdAsync(userId)).ThrowsAsync(new Exception("Simulated error"));

            // Act
            var result = await userController.GetUserById(userId);

            // Assert
            Assert.IsInstanceOf<ObjectResult>(result.Result);
            var objectResult = result.Result as ObjectResult;
            Assert.That(objectResult.StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
            Assert.That(objectResult.Value, Is.EqualTo("Error retrieving data from the database"));
        }

        [Test]
        public async Task CreateUser_ValidUser_ReturnsOkResultWithCreatedUser()
        {
            // Arrange
            var newUser = new User
            {
                Id = 1,
                Username = "JD1234",
                Password = "Password1"
            };
            var createdUser = new User
            {
                Id = 1,
                Username = "JD1234",
                Password = "Password1"
            };
            mockService.Setup(service => service.CreateUserAsync(newUser)).ReturnsAsync(createdUser);

            // Act
            var result = await userController.CreateUser(newUser);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsInstanceOf<User>(okResult.Value);
            Assert.AreEqual(createdUser, okResult.Value);
            mockService.Verify(service => service.CreateUserAsync(newUser), Times.Once);
        }

        [Test]
        public async Task CreateUser_InvalidUser_ReturnsBadRequestResult()
        {
            // Arrange
            var invalidUser = new User(); // Missing required properties
            mockService.Setup(service => service.CreateUserAsync(invalidUser)).ReturnsAsync(null as User);

            // Act
            var result = await userController.CreateUser(invalidUser);

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(result.Result);
            mockService.Verify(service => service.CreateUserAsync(invalidUser), Times.Once);
        }

        [Test]
        public async Task CreateUser_Exception_ReturnsStatusCode500()
        {
            // Arrange
            var newUser = new User { Username = "New User"};
            mockService.Setup(service => service.CreateUserAsync(newUser)).ThrowsAsync(new Exception("Simulated error"));

            // Act
            var result = await userController.CreateUser(newUser);

            // Assert
            Assert.IsInstanceOf<ObjectResult>(result.Result);
            var objectResult = result.Result as ObjectResult;
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
            Assert.AreEqual("Error creating new user record", objectResult.Value);
            mockService.Verify(service => service.CreateUserAsync(newUser), Times.Once);
        }

        [Test]
        public async Task UpdateUser_ExistingUser_ReturnsOkResultWithUpdatedUser()
        {
            // Arrange
            int userId = 1;
            var existingUser = new User
            {
                Id = 1,
                Username = "JD1234",
                Password = "Password1"
            };

            var updatedUser = new User
            {
                Id = 1,
                Username = "JD1234",
                Password = "Password12"
            };

            mockService.Setup(service => service.GetUserByIdAsync(userId)).ReturnsAsync(existingUser);
            mockService.Setup(service => service.UpdateUserAsync(userId, updatedUser)).ReturnsAsync(updatedUser);

            // Act
            var result = await userController.UpdateUser(userId, updatedUser);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            mockService.Verify(service => service.GetUserByIdAsync(userId), Times.Once);
            mockService.Verify(service => service.UpdateUserAsync(userId, updatedUser), Times.Once);


        }

    }
}

