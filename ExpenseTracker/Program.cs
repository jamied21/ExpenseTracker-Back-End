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
        builder.Services.AddScoped<IExpenseCategoryRepository, ExpenseCategoryRepository>();
        builder.Services.AddScoped<IExpenseCategoryService, ExpenseCategoryService>();


        //builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
        //{
        //    var origins = builder.Configuration.GetSection("AllowedCorsOrigins").Get<string[]>();
        //    policy.WithOrigins(origins).AllowAnyMethod().AllowAnyHeader();
        //}));

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        //builder.Services.AddCors(options =>
        //{
        //    options.AddPolicy("CorsPolicy",
        //        builder => builder.AllowAnyOrigin()
        //        .AllowAnyMethod()
        //        .AllowAnyHeader()
        //        );
        //});

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("ReactApp", builder =>
            {
                builder.WithOrigins("http://localhost:3000") 
                       .AllowAnyHeader()
                       .AllowAnyMethod();
            });
        });


        var app = builder.Build();


        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }



        app.UseCors("ReactApp");

        //  app.UseCors("CorsPolicy");

        //app.UseCors(builder =>
        //{
        //    builder
        //    .WithOrigins(new string[] { "http://localhost" })
        //    .AllowAnyMethod()
        //    .AllowAnyHeader()
        //    .AllowCredentials();
        //});

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
      //  app.MapGet("/", () => "Hello World!");

        app.Run();
    }
}

