using System.Linq;
using Microsoft.EntityFrameworkCore;
using TODOLIST.Model.Entities;

namespace TODOLIST.Data
{
    public class TodolistContext : DbContext
    {
        public DbSet<Sticker> Users { get; set; }
        
        public TodolistContext(DbContextOptions<TodolistContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            ConfigureModelBuilderForGame(modelBuilder);
        }

        private void ConfigureModelBuilderForGame(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sticker>().ToTable("Sticker");
        }
    }
}
