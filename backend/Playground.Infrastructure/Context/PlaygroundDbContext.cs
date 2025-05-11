using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Playground.Domain.Common;
using Playground.Domain.Entities;
using Playground.Domain.ValueObjects;

namespace Playground.Infrastructure.Context;

public class PlaygroundDbContext : DbContext
{
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<IndirectCost> IndirectCosts { get; set; }

    public DbSet<User> Users { get; set; }

    public PlaygroundDbContext(DbContextOptions<PlaygroundDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Restaurant
        modelBuilder.Entity<Restaurant>()
            .HasKey(r => r.RestaurantId);

        modelBuilder.Entity<Restaurant>()
            .HasMany(r => r.IndirectCosts)
            .WithOne(ic => ic.Restaurant)
            .HasForeignKey(ic => ic.RestaurantId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Restaurant>()
            .HasMany(r => r.Recipes)
            .WithOne(r => r.Restaurant)
            .HasForeignKey(r => r.RestaurantId)
            .OnDelete(DeleteBehavior.Cascade);

        // User
        modelBuilder.Entity<User>()
            .HasOne(u => u.Restaurant)
            .WithMany(r => r.Owners)
            .HasForeignKey(u => u.RestaurantId);

        // IndirectCost
        modelBuilder.Entity<IndirectCost>()
            .HasKey(ic => ic.IndirectCostId);

        modelBuilder.Entity<IndirectCost>()
            .OwnsOne(ic => ic.Value, mv =>
            {
                mv.Property(m => m.Amount).HasColumnName("Value_Amount");
                mv.Property(m => m.Currency).HasColumnName("Value_Currency");
            });

        modelBuilder.Entity<IndirectCost>()
            .OwnsOne(ic => ic.Period, p =>
            {
                p.Property(x => x.Type).HasColumnName("PeriodType");
            });

        modelBuilder.Entity<IndirectCost>()
            .OwnsOne(ic => ic.Category, c =>
            {
                c.Property(x => x.Name).HasColumnName("CategoryName");
            });

        // Ingredient
        modelBuilder.Entity<Ingredient>()
            .HasKey(i => i.IngredientId);

        modelBuilder.Entity<Ingredient>()
            .OwnsOne(i => i.UnitPrice, up =>
            {
                up.Property(x => x.Amount).HasColumnName("UnitPrice_Amount");
                up.Property(x => x.Currency).HasColumnName("UnitPrice_Currency");
            });

        modelBuilder.Entity<Ingredient>()
            .OwnsOne(i => i.Unit, u =>
            {
                u.Property(x => x.Symbol).HasColumnName("UnitSymbol");
            });

        // Recipe
        modelBuilder.Entity<Recipe>()
            .HasKey(r => r.RecipeId);

        modelBuilder.Entity<Recipe>()
            .OwnsOne(r => r.ProfitMargin, p =>
            {
                p.Property(x => x.Value).HasColumnName("ProfitMargin");
            });

        modelBuilder.Entity<Recipe>()
            .HasMany(r => r.Ingredients)
            .WithOne(ri => ri.Recipe)
            .HasForeignKey(ri => ri.RecipeId)
            .OnDelete(DeleteBehavior.Cascade);

        // RecipeIngredient
        modelBuilder.Entity<RecipeIngredient>()
            .HasKey(ri => ri.RecipeIngredientId);

        modelBuilder.Entity<RecipeIngredient>()
            .HasOne(ri => ri.Ingredient)
            .WithMany()
            .HasForeignKey(ri => ri.IngredientId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RecipeIngredient>()
            .OwnsOne(ri => ri.QuantityUnit, u =>
            {
                u.Property(x => x.Symbol).HasColumnName("QuantityUnitSymbol");
            });

        modelBuilder.Entity<RecipeIngredient>()
            .OwnsOne(ri => ri.UnitCost, c =>
            {
                c.Property(x => x.Amount).HasColumnName("UnitCost_Amount");
                c.Property(x => x.Currency).HasColumnName("UnitCost_Currency");
            });
    }


    public override int SaveChanges()
    {
        return base.SaveChanges();
    }


    /// <summary>
    /// Atualiza campos de auditoria
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.UpdateTimestamps();
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdateTimestamps();
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }

}
