using Microsoft.EntityFrameworkCore;
using App.Models;

namespace App.Contexts
{
    public static class DataSeedingExtensions
    {
        public static WebApplication UseSeeding(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            if (!context.ExerciseTypes.Any())
            {
                context.ExerciseTypes.AddRange(
                    new ExerciseType { Id = 1, Name = "фул бади", CreatedAt = DateTime.UtcNow },
                    new ExerciseType { Id = 2, Name = "ноги",  CreatedAt = DateTime.UtcNow }
                );
                context.SaveChanges();
            }
            if (!context.Exercises.Any())
            {
                context.Exercises.AddRange(
                    new Exercise { Id = 1, Name = "Отжимания", CreatedAt = DateTime.UtcNow, ExerciseTypeId = 1 },
                    new Exercise { Id = 2, Name = "Приседания", CreatedAt = DateTime.UtcNow, ExerciseTypeId = 2 }
                );
                context.SaveChanges();
            }

            return app;
        }
        
        public static async Task<WebApplication> UseAsyncSeeding(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            if (!await context.ExerciseTypes.AnyAsync())
            {
                await context.ExerciseTypes.AddRangeAsync(
                    new ExerciseType { Id = 1, Name = "фул бади", CreatedAt = DateTime.UtcNow },
                    new ExerciseType { Id = 2, Name = "ноги", CreatedAt = DateTime.UtcNow }
                );
                await context.SaveChangesAsync();
            }

            if (!await context.Exercises.AnyAsync())
            {
                await context.Exercises.AddRangeAsync(
                    new Exercise { Id = 1, Name = "Отжимания", CreatedAt = DateTime.UtcNow, ExerciseTypeId = 1 },
                    new Exercise { Id = 2, Name = "Приседания", CreatedAt = DateTime.UtcNow, ExerciseTypeId = 2 }
                );
                await context.SaveChangesAsync();
            }

            return app;
        }
    }
}