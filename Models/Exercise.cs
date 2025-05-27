namespace App.Models
{
    public class Exercise
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; } = null!;
        public long ExerciseTypeId { get; set; }
        public ExerciseType ExerciseType { get; set; } = null!;
    }
}
