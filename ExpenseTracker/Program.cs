using ExpenseTracker.Data;
using ExpenseTracker.Repo;
using ExpenseTracker.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ExpenseTracker;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Done to access configuration settings
        var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

        // PostgreSQL setup
        var connectionString = configuration.GetConnectionString("ExpenseTrackerDB");

        builder.Services.AddDbContext<ExpenseTrackerContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("ExpenseTrackerDB"));
        });
        
        builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
        builder.Services.AddScoped<IExpenseService, ExpenseService>();


        builder.Services.AddControllers();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.UseHttpsRedirection();
        app.UseAuthorization();

        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.MapGet("/", () => "Hello World!");

        app.Run();
    }
}

