using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ExpenseTracker.Models
{
	public class Expense
	{
		public int Id { get; set; }

        [Required(ErrorMessage = "The Name field is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Amount field is required.")]
        public decimal Amount { get; set; }

		public DateTime ExpenseDate { get; set; }

        public int? UserId { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }

       
        public int? CategoryId { get; set; }

        [JsonIgnore]
        public virtual ExpenseCategory Category { get; set; }

      





    }
}

