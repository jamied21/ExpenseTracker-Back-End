using System;
using System.Collections.Generic;

namespace ExpenseTracker.Models
{
	public class ExpenseCategory
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();
	}
}

