using FoodApp.Model;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Text.Json;

namespace FoodApp.ModuleDB
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Orders_Positions> Orders_Positions { get; set; }


        private string jsonPath = @"../../../ModuleDB/connectionConfig.json";
        private string json = String.Empty;

        public string connectionString = String.Empty;

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Order>()
                .HasMany(e => e.Positions)
                .WithMany(e => e.Orders)
                .UsingEntity<Orders_Positions>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            using (StreamReader read = new(jsonPath))
            {
                json = read.ReadToEnd();
            }

            var option = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            Connection connection = JsonSerializer.Deserialize<Connection>(json, option);
            connectionString = connection.ConnectionString;
            //code for parse from json
            optionsBuilder.UseSqlServer(connectionString); //json connection string need here
        }
    }
}
