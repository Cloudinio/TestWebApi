using Microsoft.EntityFrameworkCore;
using App.Models;

namespace App.Contexts
{
    public class ApplicationDbContext : DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ExerciseType> ExerciseTypes { get; set; } 

        public DbSet<Exercise> Exercises { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            base.OnModelCreating(modelBuilder); 

            modelBuilder.Entity<ExerciseType>(entity => 
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.CreatedAt)
                      .IsRequired();                 

                entity.Property(e => e.Name)
                      .IsRequired();

                entity.HasIndex(e => e.Name)
                      .IsUnique();

                entity.HasMany(e => e.Exercises)
                      .WithOne(ex => ex.ExerciseType)
                      .HasForeignKey(ex => ex.ExerciseTypeId);
            });

            modelBuilder.Entity<Exercise>(entity => 
            {
                entity.HasKey(e => e.Id);               

                entity.Property(e => e.CreatedAt)
                      .IsRequired();                

                entity.Property(e => e.Name)
                      .IsRequired();                

                entity.HasIndex(e => e.Name)
                      .IsUnique();                  

                entity.HasOne(e => e.ExerciseType)
                      .WithMany(et => et.Exercises)
                      .HasForeignKey(e => e.ExerciseTypeId);
            });
        }
    }
}