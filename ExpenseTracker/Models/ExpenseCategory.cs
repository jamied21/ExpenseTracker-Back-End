﻿using System;
namespace ExpenseTracker.Models
{
	public class ExpenseCategory
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public int ExpenseId { get; set; }

		public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
	}
}
