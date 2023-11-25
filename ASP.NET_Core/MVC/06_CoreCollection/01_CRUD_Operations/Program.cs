﻿using _01_CRUD_Operations.Models;
using Microsoft.EntityFrameworkCore;

namespace _01_CRUD_Operations;

public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        // Добавление контекста бд
        builder.Services.AddDbContext<TransactionDbContext>(option => {
            option.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionSQL"));
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment()) {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Transactions}/{action=Index}/{id?}");

        app.Run();
    }
}