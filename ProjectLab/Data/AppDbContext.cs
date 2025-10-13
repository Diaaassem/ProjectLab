using Microsoft.EntityFrameworkCore;
using ProjectLab.Models;
using System;

namespace ProjectLab.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<InstructorCourse> InstructorCourses { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=MvcLab;Trusted_Connection=True; TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.SSN);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Age).IsRequired();
                entity.Property(e => e.Address).HasMaxLength(200);
                entity.Property(e => e.Image).IsRequired();
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.HasOne(e => e.Department)
                      .WithMany(d => d.Students)
                      .HasForeignKey(e => e.DeptId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.DeptId);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Location).HasMaxLength(100);
                entity.Property(e => e.Manager).HasMaxLength(100);
                entity.HasMany(e => e.Students)
                      .WithOne(s => s.Department)
                      .HasForeignKey(s => s.DeptId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Instructor>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Age).IsRequired();
                entity.Property(e => e.Salary).IsRequired();
                entity.Property(e => e.Image).IsRequired();
                entity.Property(e => e.HireDate).IsRequired();
                entity.Property(e => e.Address).HasMaxLength(200);
                entity.HasOne(e => e.Department)
                      .WithMany(d => d.Instructors)
                      .HasForeignKey(e => e.DeptId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Course>(entity =>
            {
               entity.HasKey(c => c.Id);
               entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
               entity.Property(c => c.Topic).IsRequired();
               entity.Property(c => c.Degree).IsRequired();
               entity.Property(c => c.MinDegree).IsRequired();

            });

            modelBuilder.Entity<StudentCourse>(entity =>
            {
               entity.HasKey(sc => new { sc.StudentSSN, sc.CourseId });

               entity.HasOne(sc => sc.Student)
                     .WithMany(s => s.Registrations)
                     .HasForeignKey(sc => sc.StudentSSN);

               entity.HasOne(sc => sc.Course)
                     .WithMany(c => c.Registrations)
                     .HasForeignKey(sc => sc.CourseId);

                entity.Property(sc => sc.Grade).IsRequired(false);
            });

            modelBuilder.Entity<InstructorCourse>(entity =>
            {
               entity.HasKey(ic => new { ic.InstructorId, ic.CourseId });

               entity.HasOne(ic => ic.Instructor)
                     .WithMany(i => i.TeachCourses)
                     .HasForeignKey(ic => ic.InstructorId);

               entity.HasOne(ic => ic.Course)
                     .WithMany(c => c.TeachCourses)
                     .HasForeignKey(ic => ic.CourseId);

                entity.Property(ic => ic.RateHour).IsRequired();
            });



            /******************************************************************************************************************/
            /******************************************************************************************************************/

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
