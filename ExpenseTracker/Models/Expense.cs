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


    //Will add in once login page is done
      //  public int? UserId { get; set; }

      
       
        public int? CategoryId { get; set; }

       
      





    }
}

