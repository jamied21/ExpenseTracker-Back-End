using System;
namespace ExpenseTracker.Models
{
	public class User
	{
		public int Id { get; set; }

		public string Username { get; set; }

		public string Password { get; set; }

		public ICollection<Expense> Expenses { get; set; } = new List<Expense>();

	}
}

