using Microsoft.EntityFrameworkCore;

namespace _14_EntityFrameworkCore;

public class ApplicationContext : DbContext {

    /// <summary> 
    /// Свойство DbSet представляет собой коллекцию объектов, 
    /// которая сопоставляется с определенной таблицей в базе данных. 
    /// То есть свойство Users будет представлять таблицу, в которой будут храниться объекты User.
    /// </summary>
    public DbSet<User> Users { get; set; } = null!;

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) {
        Database.EnsureCreated();   // создаем базу данных при первом обращении
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Tom", Age = 37 },
                new User { Id = 2, Name = "Bob", Age = 41 },
                new User { Id = 3, Name = "Sam", Age = 24 }
        );
    }
}