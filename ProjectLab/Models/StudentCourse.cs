namespace ProjectLab.Models
{
    public class StudentCourse
    {
        public int StudentSSN { get; set; }
        public int CourseId { get; set; }
        public string Grade { get; set; }
        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}
