using System;
using ExpenseTracker.Models;
using ExpenseTracker.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controller
{
    [ApiController]
    [Route("api/[controller]")]
	public class UserController: ControllerBase
    {
		private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        #region GetUsers

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            try

            {
                return Ok(await userService.GetAllUsersAsync());

            }

            catch (Exception)

            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }

        }
        #endregion GetUsers

        #region GetUserById
        [HttpGet("{id:int}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            try
            {
                var user = await userService.GetUserByIdAsync(id);

                if (user == null)
                {
                    return NotFound($"User with Id = {id} not found");
                }

                return Ok(user);


            }

            catch (Exception)

            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }

        }
        #endregion GetUserById

        #region CreateUser
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {

            try
            {
                var result = await userService.CreateUserAsync(user);

                if (result == null)
                {
                    return BadRequest();

                }

                return Ok(result);

            }

            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new user record");
            }


        }
        #endregion CreateExpense

        #region UpdateUser

        [HttpPut("{id:int}")]
        public async Task<ActionResult<User>> UpdateUser(int id, User user)
        {
            try
            {
                var userToUpdate = await userService.GetUserByIdAsync(id);

                if (userToUpdate == null)

                {
                    return NotFound($"Expense with Id = {id} not found");

                }

                if (id != user.Id)
                {

                    return BadRequest("Expense ID mismatch");
                }

                return Ok(await userService.UpdateUserAsync(id, user));

            }


            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }
        #endregion UpdateUser

        #region DeleteUser
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<User>> DeleteExpense(int id)

        {

            try
            {
                var userToDelete = userService.GetUserByIdAsync(id);

                if (userToDelete == null)

                {
                    return NotFound($"Expense with Id = {id} not found");

                }

                await userService.DeleteUserAsync(id);
                return Accepted();

            }

            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }


        }


        #endregion DeleteUser

        #region FindUserByUsername
        [HttpGet("username/{username}")]
        public async Task<ActionResult<User>> GetUserByUsername(string username)
        {
            var user = await userService.GetUserByUsernameAsync(username);
            if (user == null)
            {
                return NotFound($"User with Username = {username} not found");
            }
            return Ok(user);
        }
        #endregion FindUserByUsername
    }
}

