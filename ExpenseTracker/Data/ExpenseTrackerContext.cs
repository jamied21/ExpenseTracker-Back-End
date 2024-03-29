﻿using System;
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

            modelBuilder.Entity<ExpenseCategory>()
             .HasMany(c => c.Expenses)
             .WithOne()
             .HasForeignKey(e => e.CategoryId);
            //.IsRequired();


        // To be added in once login page is setup and authentication is done
           // modelBuilder.Entity<User>()
           //.HasMany(c => c.Expenses)
           //  .WithOne()
           //  .HasForeignKey(e => e.UserId);
           // //.IsRequired();

        }


    }
}

