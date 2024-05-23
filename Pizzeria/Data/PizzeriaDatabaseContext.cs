using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Pizzeria.Models;

namespace Pizzeria.Data
{
    public class PizzeriaDatabaseContext : IdentityDbContext<IdentityUser>
    {

        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Ingredient> Ingredients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=Pizzeria;Integrated Security=True;Pooling=False;Encrypt=False;TrustServerCertificate=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pizza>()
                .HasOne(o => o.Category)
                .WithMany(o => o.Pizzas)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Pizza>()
                .HasMany(p => p.Ingredients)
                .WithMany(i => i.Pizzas)
                .UsingEntity<Dictionary<string, object>>(
                    "IngredientPizza",
                    j => j
                        .HasOne<Ingredient>()
                        .WithMany()
                        .HasForeignKey("ingredientsIngredientId")
                        .OnDelete(DeleteBehavior.Cascade),
                    j => j
                        .HasOne<Pizza>()
                        .WithMany()
                        .HasForeignKey("PizzasPizzaId")
                        .OnDelete(DeleteBehavior.Cascade)
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
