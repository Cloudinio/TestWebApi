namespace App.Models
{
    public class ExerciseType
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<Exercise> Exercises { get; set; } = new List<Exercise>(); 
    }
}