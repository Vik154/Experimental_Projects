using Microsoft.EntityFrameworkCore;

namespace _09_EntityFramework.Models;

public class ApplicationContext : DbContext {
    public DbSet<User> Users { get; set; } = null!;

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options) {

        Database.EnsureCreated();   // создаем базу данных при первом обращении
    }
}
