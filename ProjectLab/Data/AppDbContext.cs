using Microsoft.EntityFrameworkCore;
using ProjectLab.Models;
using System;

namespace ProjectLab.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=MvcLab;Trusted_Connection=True; TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasKey(s => s.SSN);
            modelBuilder.Entity<Student>().Property(s => s.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Student>().Property(s => s.Age).IsRequired();
            modelBuilder.Entity<Student>().Property(s => s.Address).HasMaxLength(200);
            modelBuilder.Entity<Student>().Property(s => s.Image).IsRequired();
            modelBuilder.Entity<Student>().Property(s => s.Email).HasMaxLength(100);

            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    SSN = 1,
                    Name = "Alice Johnson",
                    Age = 20,
                    Image = "alice.jpg",
                    Address = "123 Maple Street, Springfield",
                    Email = "alice.johnson@example.com"
                },
                new Student
                {
                    SSN = 2,
                    Name = "Benjamin Carter",
                    Age = 22,
                    Image = "benjamin.jpg",
                    Address = "456 Oak Avenue, Lincoln",
                    Email = "ben.carter@example.com"
                },
                new Student
                {
                    SSN = 3,
                    Name = "Chloe Martinez",
                    Age = 19,
                    Image = "chloe.jpg",
                    Address = "789 Pine Road, Riverside",
                    Email = "chloe.martinez@example.com"
                },
                new Student
                {
                    SSN = 4,
                    Name = "Daniel Kim",
                    Age = 21,
                    Image = "daniel.jpg",
                    Address = "321 Birch Lane, Cedar City",
                    Email = "daniel.kim@example.com"
                }
            );
        }
    }
}
