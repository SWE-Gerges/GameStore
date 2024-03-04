
using GameStore.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Data;
public class ApplicationDbContext : DbContext{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
     : base(options)
    {

    }

    public DbSet<Game> Games { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Device> Devices { get; set; }

    public DbSet<GameDevice> GameDevices { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
        .HasData(new Category[]
        {
            new Category{Id = 1, Name = "Horror"},
            new Category{Id = 2, Name = "StoryLine"},
            new Category{Id = 3, Name = "Action"},
            new Category{Id = 4, Name = "Sports"}
        });


        modelBuilder.Entity<Device>()
        .HasData(new Device[]
        {
            new Device{Id = 1, Name = "Xbox", Icon = "bi bi-xbox"},
            new Device{Id = 2, Name ="PlayStation", Icon = "bi bi-playstation"},
            new Device{Id = 3, Name = "Console", Icon = "bi bi-nintendo-switch"}
        });

        modelBuilder.Entity<GameDevice>()
        .HasKey(e => new {e.GameId, e.DeviceId});
        base.OnModelCreating(modelBuilder);
    }
}