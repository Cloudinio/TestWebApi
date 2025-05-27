namespace App.DTOs
{
    public class ExerciseTypeDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }

    public class ExerciseDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public ExerciseTypeDto Type { get; set; } = null!;
    }

    public class CreateExerciseDto
    {
        public string Name { get; set; } = null!;
        public long ExerciseTypeId { get; set; }
    }

    public class UpdateExerciseDto
    {
        public string Name { get; set; } = null!;
        public long ExerciseTypeId { get; set; }
    }
}