using BarnManagement.Models;
using System;
using System.Data.Entity;

namespace BarnManagement.Data
{
    public class BarnContext : DbContext
    {
        public BarnContext() : base("BarnDb") { }

        public DbSet<User> Users { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Barn> Barns { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>()
               .Map<Cow>(m => m.Requires("Discriminator").HasValue("Cow"))
               .Map<Chicken>(m => m.Requires("Discriminator").HasValue("Chicken"))
               .Map<Sheep>(m => m.Requires("Discriminator").HasValue("Sheep"));

           
            modelBuilder.Entity<Animal>()
                .HasRequired(a => a.Barn)
                .WithMany() 
                .HasForeignKey(a => a.BarnId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasRequired(p => p.Barn)
                .WithMany()
                .HasForeignKey(p => p.BarnId)
                .WillCascadeOnDelete(false);

            modelBuilder.Properties<DateTime>()
                        .Configure(c => c.HasColumnType("datetime2"));

            base.OnModelCreating(modelBuilder);
        }
    }
}
