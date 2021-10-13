using API.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }
        public DbSet<Case> Cases { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Convertation> Convertations { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<StatusCode> StatusCodes { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            // many to one Role -> User
            modelBuilder.Entity<Role>()
                .HasMany(r => r.User).WithOne(u => u.Role);

            // many to one StatusCode -> history
            modelBuilder.Entity<StatusCode>()
                .HasMany(sc => sc.History).WithOne(h => h.StatusCode);
            
            //many to one category -> case
            modelBuilder.Entity<Category>()
                .HasMany(ct => ct.Case).WithOne(cs => cs.Category);

            // many to one priority -> case
            modelBuilder.Entity<Priority>()
                .HasMany(p => p.Case).WithOne(cs => cs.Priority);

            // many to one case -> convertation
            modelBuilder.Entity<Case>()
                .HasMany(cs => cs.Convertation).WithOne(cv => cv.Case);

            // many to one case -> history
            modelBuilder.Entity<Case>()
                .HasMany(cs => cs.History).WithOne(h => h.Case);

            //-------------------------------
            //many to one user -> convertation, user -> case, user -> history
            modelBuilder.Entity<User>()
                .HasMany(u => u.Convertation).WithOne(cv => cv.User);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Case).WithOne(cs => cs.User);

            modelBuilder.Entity<User>()
                .HasMany(u => u.History).WithOne(h => h.User);
        }
    }
}
