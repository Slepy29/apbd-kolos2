using apbd_kolos2.Models;
using Microsoft.EntityFrameworkCore;

namespace apbd_kolos2.Data;

public class DatabaseContext : DbContext
{
    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Items> items { get; set; }
    public DbSet<Backpacks> backpacks { get; set; }
    public DbSet<Characters> characters { get; set; }
    public DbSet<Character_titles> character_titles { get; set; }
    public DbSet<Titles> titles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Items>(e =>
        {
            e.ToTable("items");
            
            e.HasKey(e => e.Id);
            e.Property(e => e.Name).HasMaxLength(100);
            
            e.HasData(new List<Items>()
            {
                new() { Id = 1, Name = "John", Weight = 1},
                new() { Id = 2, Name = "Ann", Weight = 2},
                new() { Id = 3, Name = "Jack", Weight = 3}
            });
        });
        
        modelBuilder.Entity<Backpacks>(e =>
        {
            e.ToTable("backpacks");
            
            e.HasKey(e => new { e.CharacterId, e.ItemId});
            
            e.HasOne(e => e.Item)
                .WithMany(e => e.BackpacksCollection)
                .HasForeignKey(e => e.ItemId)
                .OnDelete(DeleteBehavior.Cascade);
            
            e.HasOne(e => e.Character)
                .WithMany(e => e.BackpacksCollection)
                .HasForeignKey(e => e.CharacterId)
                .OnDelete(DeleteBehavior.Cascade);
            
            e.HasData(new List<Backpacks>()
            {
                new() { CharacterId = 1, ItemId = 1, Amount = 1},
                new() { CharacterId = 2, ItemId = 2, Amount = 1},
                new() { CharacterId = 3, ItemId = 3, Amount = 1},
            });
        });
        
        modelBuilder.Entity<Characters>(e =>
        {
            e.ToTable("characters");
            
            e.HasKey(e => e.Id);
            e.Property(e => e.FirstName).HasMaxLength(50);
            e.Property(e => e.LastName).HasMaxLength(120);
            
            e.HasData(new List<Characters>()
            {
                new() { Id = 1, FirstName = "John1", LastName = "Dough1", CurrentWei = 1, MaxWeight = 10},
                new() { Id = 2, FirstName = "John2", LastName = "Dough2", CurrentWei = 2, MaxWeight = 20},
                new() { Id = 3, FirstName = "John3", LastName = "Dough3", CurrentWei = 3, MaxWeight = 30},
            });
        });
        
        modelBuilder.Entity<Character_titles>(e =>
        {
            e.ToTable("character_titles");
            
            e.HasKey(e => new { e.CharacterId, e.TitleId});
            
            e.HasOne(e => e.Character)
                .WithMany(e => e.CharacterTitlesCollection)
                .HasForeignKey(e => e.CharacterId)
                .OnDelete(DeleteBehavior.Cascade);
            
            e.HasOne(e => e.Title)
                .WithMany(e => e.CharacterTitlesCollection)
                .HasForeignKey(e => e.TitleId)
                .OnDelete(DeleteBehavior.Cascade);
            
            e.HasData(new List<Character_titles>()
            {
                new() { CharacterId = 1, TitleId = 1, AcquireAt = new DateTime()},
                new() { CharacterId = 2, TitleId = 2, AcquireAt = new DateTime()},
                new() { CharacterId = 3, TitleId = 3, AcquireAt = new DateTime()},
            });
        });
        
        modelBuilder.Entity<Titles>(e =>
        {
            e.ToTable("titles");
            
            e.HasKey(e => e.Id);
            e.Property(e => e.Name).HasMaxLength(100);
            
            e.HasData(new List<Titles>()
            {
                new() { Id = 1, Name = "King1"},
                new() { Id = 2, Name = "King2"},
                new() { Id = 3, Name = "King3"},
            });
        });
    }
}