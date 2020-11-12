using CoreGram.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace CoreGram.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>().Property("Id").IsRequired();
            modelBuilder.Entity<User>().ToTable("Users").HasKey(x => x.Id);            
            modelBuilder.Entity<User>()
                .HasOne<UserProfile>(x => x.Profile)
                .WithOne(x => x.User)
                .HasForeignKey<UserProfile>(x => x.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<User> Users {get; set; }
        public DbSet<UserProfile> UserProfile { get; set; }

    }
}
