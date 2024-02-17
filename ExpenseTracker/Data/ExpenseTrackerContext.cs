using System;
using ExpenseTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Data
{
	public class ExpenseTrackerContext : DbContext
	{
        public ExpenseTrackerContext(DbContextOptions<ExpenseTrackerContext> options) : base(options)
        {
        }


        //protected readonly IConfiguration Configuration;

        //public ExpenseTrackerContext(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
        //    // connect to postgres with connection string from app settings
        //    options.UseNpgsql(Configuration.GetConnectionString("ExpenseTrackerDB"));
        //}

        public DbSet<Expense> Expenses {get; set;}
		public DbSet<ExpenseCategory> ExpenseCategories {get; set;}
		public DbSet<User> Users { get; set; }





        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //Define entity relationships

            modelBuilder.Entity<Expense>()
             .HasOne(e => e.Category)
             .WithMany(c => c.Expenses)
             .HasForeignKey(e => e.CategoryId);

            modelBuilder.Entity<Expense>()
           .HasOne(e => e.User)
           .WithMany(u => u.Expenses)
           .HasForeignKey(e => e.UserId);

           
        }


    }
}

